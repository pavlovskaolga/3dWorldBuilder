using UnityEngine;

namespace UnityStruckdTest.Data
{
    [System.Serializable]
    public class SerializableWorld
    {
        public SerializableObject[] Objects;
    }

    [System.Serializable]
    public struct SerializableObject
    {
        public Position Position;
        public int ID;
    }

    [System.Serializable]
    public struct Position
    {
        public float X;
        public float Y;
        public float Z;

        public static implicit operator Position(Vector3 position)
        {
            return new Position { X = position.x, Y = position.y, Z = position.z };
        }

        public static implicit operator Vector3(Position position)
        {
            return new Vector3 { x = position.X, y = position.Y, z = position.Z };
        }
    }
}