using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    // Like a stack, but you can take things out of the middle.

    public class MessageStack
    {
        readonly LinkedList<WhiteboardMessage> list = new();
        // Use this for initialization

        public void Push(WhiteboardMessage item)
        {
            list.AddFirst(item);
        }

        public void Remove(WhiteboardMessage item)
        {
            if (!list.Remove(item))
            {
                Debug.LogFormat("Asked to remove {0} when it wasn't in the list", item);
            }
        }

        public WhiteboardMessage Peek()
        {
            if (list.Count == 0) return WhiteboardMessage.INVALID_ERROR;
            return list.First.Value;
        }

        public int Count()
        {
            return list.Count;
        }
    }
}