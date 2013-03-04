using System.Collections.Generic;

namespace Editor
{
    public struct Entity
    {
        private string name;

        private List<Parameter> parameters;


        public List<Parameter> Parameters
        {
            get
            {
                if(parameters == null)
                {
                    parameters = new List<Parameter>();
                }

                return parameters;
            }
            set { parameters = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public struct Parameter
        {
            private string name;
            private ParameterType type;

            public ParameterType Type
            {
                get { return type; }
                set { type = value; }
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }
        }

    }
}