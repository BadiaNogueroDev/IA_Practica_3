using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BTs
{
    // a BehaviourTree is a Tickable ScriptableObject built around a root Node.
    // most of the Tickable services are provided through the root

    // It provides introspection services to detect parameters.

    public abstract class BehaviourTree : ScriptableObject, ITickable
    {

        // els input parameters del BT (com a metaInformació...)
        // private List<FieldInfo> inputParameters = null; // FOSSIL

        protected Node root; // value must be set in onConstruction
        public string Name { get; set; }

        // NOTA: aquest constructor l'utilitza "el sistema". Quan es fa la creació 
        // encara no es coneix el gameObject associat. Per aquesta raó no es pot
        // donar el gameObject a root.
        // tampoc no es pot invocar OnConstruction perquè aquest codi (OnConstruction)
        // assumeix que els InputParameters del BT ja estan a punt (amb els valors 
        // que hagi pogut donar l'excecutor per via de l'inspector)
        public BehaviourTree () : base ()
        {
            /* // FOSSIL CODE
             InitParameters();
            inputParameters = DiscoverInputParameters(); */
            Name = "Anonymous Behaviour tree of type " + GetType();
        }
        

        // Aquí és on es materialitza la construcció de l'estructura arbòria.
        // No es fa al constructor, es fa aquí.
        // Aquest mètode és invocat en dos llocs:
        // - a l'executor (BehaviourTreeExecutor)
        // - a NonLeafNode (quan un NonLeafNode rep el seu gameObject, fa efectiva la 
        //   construcció dels seus fills)
        public void Construct (GameObject gameObject)
        {
            gm = gameObject; // temporary so that OnConstruction has indirect visibility
            OnConstruction();
            // quan es fa la construcció -OnConstruction()- és quan root pren valor
            root.theGameObject = gameObject;
        }

        
        // -------------------------
        // this is the one and only method that subclasses must implement.
        // -------------------------
        public abstract void OnConstruction();


        // ------------------------------------------
        // implementation of the Tickable interface
        // ------------------------------------------
        // Notice how tickable services are "delegated" to root (Node)

        public Status Tick()
        {
            return root.Tick();
        }

        public void Abort()
        {
            root.Abort();
        }

        public void Clear()
        {
            root.Clear();
        }


        public Status GetStatus()
        {
            // this method should not be invoked on non-created (root==null) monobehaviours
            return root.GetStatus();
        }

        public bool IsTerminated ()
        {
            return root.IsTerminated();
        }

        // GameObject "propagation".
        // Useful in lambdas...
        private GameObject gm;
        public GameObject gameObject
        {
            get {
                if (root != null && root.theGameObject != null)
                    return root.theGameObject;
                else return gm;
            }
        }


        /* FOSSIL CODE

        // -------------------------------------
        //           INTROSPECTION
        // -------------------------------------

        // --- post creation reflection-based method that identifies InputParameter and
        // --- OutputParameters and initializes them if not previously initialized
        private void InitParameters()
        {
            // get the actual type and a list of public fields from it...
            FieldInfo[] fields = this.GetType().GetFields();
            // iterate over the whole set of fields
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsGenericType) // if it's a type with <> ...
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
                    }
                }
            }
        }


        // introspective discovery of inputparameters... 

        public List<FieldInfo> DiscoverInputParameters()
        {
            List<FieldInfo> result = new List<FieldInfo>();

            // get the actual type and a list of public fields from it...
            FieldInfo[] fields = this.GetType().GetFields();
            // iterate over the whole set of fields
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsGenericType) // if it's a type with <> ...
                {
                    Type genericTDef = field.FieldType.GetGenericTypeDefinition();
                    if (genericTDef.Equals(typeof(InputParameter<>)))
                    {
                        // input parameted detected
                        result.Add(field);

                        // if not set, set it
                        if (field.GetValue(this) == null)
                            field.SetValue(this, Activator.CreateInstance(field.FieldType));

                    }
                }
            }
            return result;
        } // end of introspective DiscoverInputParameters

        // introspective setter for the value of an InputParameter
        // used by the executor when it "discovers" (is notified) that the inspector
        // has changed a value
        public void PushObject(int pos, object val)
        {
            ((IValuable)inputParameters[pos].GetValue(this)).SetValue(val);
        }

        */

        public T GetComponent<T>()
        {
            return gameObject.GetComponent<T>();
        }

        public T AddComponent<T>() where T : Component
        {
            return gameObject.AddComponent<T>();
        }

    }

}
