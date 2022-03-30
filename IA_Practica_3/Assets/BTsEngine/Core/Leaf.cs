using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace BTs
{
    public abstract class Leaf : Node
    {

        public Leaf() : this("unnamed leaf") { }
        public Leaf(string name) : base(name)
        {
            // InitParameters(); // FOSSIL CODE
        } // final del constructor


        //private List<IClearable> outputParameters; // a list containing output parameters. FOSSIL

        // the blackboard that materializes the context of the gameobject 
        // and hence the context where the leaf is ticked
        public Blackboard blackboard
        {
            get { return theGameObject.GetComponent<Blackboard>(); }
        }

        public GameObject gameObject
        {
            get { return theGameObject; }
            /*
            set { theGameObject = value;
                Debug.Log("Leaf " + Name + " is setting its gameobject to " + value.name);
                }
            */
        }

        // should'nt this Initialize be "sealed" against overriding???
        public override void Initialize()
        {
            status = Status.RUNNING;
            OnInitialize();
            // ClearOutputParameters(); // FOSSIL CODE
        }

        public override void OnAbort() { }

        // "users" should override this method 
        public override Status OnTick()
        {
            return Status.FAILED;
        }

        // "users" can override this method in functions that have "state"
        public virtual void OnInitialize() { }

        /* FOSSIL CODE
        // --- post creation reflection-based method that identifies InputParameter and
        // --- OutputParameters and initializes them if not previously initialized
        private void InitParameters()
        {
            // get the actual type and a list of public fields from it...
            FieldInfo[] fields = this.GetType().GetFields();
            // iterate over the whole set of fields
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsGenericType)
                {
                    Type genericTDef = field.FieldType.GetGenericTypeDefinition();
                    if (genericTDef.Equals(typeof(InputParameter<>)) ||
                        genericTDef.Equals(typeof(OutputParameter<>)))
                    {
                        object value = field.GetValue(this);
                        if (value == null)
                        {
                            // InputParameter or OutputParameter declared but not set...
                            // Create an instance and set it
                            value = Activator.CreateInstance(field.FieldType);
                            field.SetValue(this, value);
                        }
                        // save the parameter for further referencing...
                        if (genericTDef.Equals(typeof(OutputParameter<>))) {
                            if (outputParameters == null) outputParameters = new List<IClearable>();
                            outputParameters.Add((IClearable)value);
                        }
                    }
                }
            }
        } // end of init parameters

        private void ClearOutputParameters ()
        {
            if (outputParameters != null)
                foreach (IClearable outpar in outputParameters)
                    outpar.Clear();
        }
        */
    }
}
