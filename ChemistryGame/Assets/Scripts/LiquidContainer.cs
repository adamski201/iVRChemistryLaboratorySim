using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidContainer : MonoBehaviour
{
    /* Rough simulation of liquids which can be used across any type of liquid container.
       
       LIMITATIONS: - note that the script is designed to work with only cylindrical GameObjects as liquids.
                      A different implementation with the same functionality would be significantly more difficult.
                    - currently containers can only be receivers or depositers. Depositers fill up receivers with
                      liquid. But a container that can deposit cannot receive, and a receiver cannot deposit. Depositers also
                      cant fill up other depositers. In this project, the beaker is the depositer and the flask is the receiver.
                      This is potentially an easy problem to fix but as it did not affect the reflux procedure it's not something
                      I focused on.
                    - Pouring liquids is currently handled by another script. If I had more time I would refactor the two scripts 
                      so that one script handles all the functionality.
    */

    /* To create a liquid follow these steps: 
     * (1) Assign this script to a Liquid Container object.
     * (2) Create a child GameObject on the Liquid Container, using a primitive cylinder. This will be the liquid.
     *     Scale it to fit in your Liquid Container, and assign it in the hierarchy. Set the transform of the liquid
     *     to a y-scale of 0 if you want it to be empty at the start.
     * (3) Assign values in the inspector.
     * 
     * RECEIVER:
     * (1) Create an empty child GameObject on the container, named PourPoint. Assign a tag named PourPoint. 
     * (2) Add a sphere collider to PourPoint, and enable IsTrigger. You want it to be positioned at the point
     *     where you would fill the container up from, with a sensible radius.
     * 
     * DEPOSITER:
     * (1) Add the Pour Liquid script to the Container and follow the instructions.
     * 
     * OPTIONAL: If you want to enable the ability to add granules:
     * (1) Add a box collider to the Container and enable IsTrigger. Set Tag to SolidEntryPoint.
     * (2) Position the collider to a point where granules will drop down and be absorbed on contact.
     */

    // This represents the maximum y-scale (height) of the liquid that you want it to fill up to.
    public float maxLiquidYScale = 0.035f;

    // This is the axis pointing "up" in the local space of the Liquid Container. Takes "x", "y", or "z".
    public string upAxis;

    // This is the liquid (cylinder) GameObject.
    public Transform liquid;

    // This is the name of the fluid. This is used to differentiate liquids from each other.
    public string fluidName;

    // If set to true, the liquid will not deplete even when it is poured into another container.
    public bool isInfinite;

    // Is true when granules are inserted into the top of the liquid.
    [HideInInspector] public bool containsAntiBumpGranules;

    // These control the rate of emptying/filling the container.
    public Vector3 emptyScaleChange = new(0, 0.001f, 0);
    public Vector3 fillScaleChange = new(0, 0.0005f, 0);

    // The mesh of the liquid. This is used to get the material (appearance) of the liquid.
    private MeshRenderer liquidMesh;

    // ----------------------------FUNCTIONS--------------------------------------------------------------------------------------------------------


    // Start is called before the first frame update
    private void Start()
    {
        // Initialize liquidMesh object
        liquidMesh = liquid.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        ResetWhenEmpty();

        // Depletes liquid when container is inverted
        if (!IsUpright())
        {
            EmptyContainer();
        }        
    }

    // Fills the container with a liquid
    public void FillContainer(string newliquidName, Material newliquidMaterial)
    {
        // If empty, the liquid name and material is copied over from the depositing container
        if (IsEmpty())
        {
            fluidName = newliquidName;
            liquidMesh.material = newliquidMaterial;
        }

        // If the depositing liquid is the same as the liquid currently in the container...
        if (fluidName == newliquidName)
        {
            // ... And the container isn't full, fill up the container
            if (!IsFull())
            {
                liquid.localScale += fillScaleChange;

                if (upAxis == "z")
                {
                    liquid.localPosition += new Vector3(0, 0, fillScaleChange.y);
                } 
                else if (upAxis == "y")
                {
                    liquid.localPosition += new Vector3(0, fillScaleChange.y, 0);
                }
                else if (upAxis == "x")
                {
                    liquid.localPosition += new Vector3(fillScaleChange.y, 0, 0);
                }
            }
        }
    }

    // Compares current y-scale to max y-scale to check if container is full
    public bool IsFull()
    {
        if (liquid.lossyScale.y >= maxLiquidYScale)
        {
            return true;
        }

        return false;
    }

    public bool IsEmpty()
    {
        if (liquid.lossyScale.y <= 0)
        {
            return true;
        }

        return false;
    }

    // Checks if the container is upright or inverted
    private bool IsUpright()
    {
        if (upAxis == "z")
        {
            if (transform.forward.y < -0.5)
            {
                return false;
            }
        }

        if (upAxis == "y")
        {
            if (transform.up.y < -0.5)
            {
                return false;
            }
        }

        return true;
    }

    public float GetYScale()
    {
        return liquid.lossyScale.y;
    }

    public void EmptyContainer()
    {
        if (!isInfinite)
        {
            if (!IsEmpty())
            {
                liquid.localScale -= emptyScaleChange;

                if (upAxis == "z")
                {
                    liquid.localPosition -= new Vector3(0, 0, emptyScaleChange.y);
                } else if (upAxis == "y")
                {
                    liquid.localPosition -= new Vector3(0, emptyScaleChange.y, 0);
                } else if (upAxis == "x")
                {
                    liquid.localPosition -= new Vector3(emptyScaleChange.y, 0, 0);
                }
            }
        }
    }

    // When the liquid is depleted, it is hidden (to prevent a visual bug)
    private void ResetWhenEmpty()
    {
        if (IsEmpty())
        {
            liquid.gameObject.SetActive(false);
            fluidName = null;
        }
        else
        {
            liquid.gameObject.SetActive(true);
        }
    }

    // This is triggered by the GranuleController script
    public void addBumpGranules()
    {
        containsAntiBumpGranules = true;
    }
}
