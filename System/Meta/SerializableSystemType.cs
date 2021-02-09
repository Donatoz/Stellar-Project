using System;
using UnityEngine;

namespace Metozis.System.Meta
{
    [Serializable]
    public class SerializableSystemType
    {
        [SerializeField]
        private string m_Name;
	
        public string Name
        {
            get { return m_Name; }
        }
	
        [SerializeField]
        private string m_AssemblyQualifiedName;
	
        public string AssemblyQualifiedName
        {
            get { return m_AssemblyQualifiedName; }
        }
	
        [SerializeField]
        private string m_AssemblyName;
	
        public string AssemblyName
        {
            get { return m_AssemblyName; }
        }
	
        private Type m_SystemType;	
        public Type SystemType
        {
            get 	
            {
                if (m_SystemType == null)	
                {
                    GetSystemType();
                }
                return m_SystemType;
            }
        }
	
        private void GetSystemType()
        {
            m_SystemType = Type.GetType(m_AssemblyQualifiedName);
        }
	
        public SerializableSystemType(Type _SystemType)
        {
            m_SystemType = _SystemType;
            m_Name = _SystemType.Name;
            m_AssemblyQualifiedName = _SystemType.AssemblyQualifiedName;
            m_AssemblyName = _SystemType.Assembly.FullName;
        }
	
        public override bool Equals(object obj)
        {
            SerializableSystemType temp = obj as SerializableSystemType;
            if ((object)temp == null)
            {
                return false;
            }
            return this.Equals(temp);
        }
	
        public bool Equals(SerializableSystemType _Object)
        {
            //return m_AssemblyQualifiedName.Equals(_Object.m_AssemblyQualifiedName);
            return _Object.SystemType.Equals(SystemType);
        }
	
        public static bool operator ==( SerializableSystemType a, SerializableSystemType b )
        {
            // If both are null, or both are same instance, return true.
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }
	
            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
	
            return a.Equals(b);
        }
	
        public static bool operator !=( SerializableSystemType a, SerializableSystemType b )
        {
            return !(a == b);
        }
    }
}