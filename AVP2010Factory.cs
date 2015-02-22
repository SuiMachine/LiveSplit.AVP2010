using System.Reflection;
using LiveSplit.AVP2010;
using LiveSplit.UI.Components;
using System;
using LiveSplit.Model;

[assembly: ComponentFactory(typeof(TwoWorldsFactory))]

namespace LiveSplit.AVP2010
{
    public class TwoWorldsFactory : IComponentFactory
    {
        public string ComponentName
        {
            get { return "AVP2010"; }
        }

        public string Description
        {
            get { return "Load time remover for Aliens vs. Predator (2010)"; }
        }

        public ComponentCategory Category
        {
            get { return ComponentCategory.Control; }
        }

        public IComponent Create(LiveSplitState state)
        {
            return new TwoWorldsComponent(state);
        }

        public string UpdateName
        {
            get { return this.ComponentName; }
        }

        public string UpdateURL
        {
            get { return ""; }
        }

        public Version Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        public string XMLURL
        {
            get { return this.UpdateURL + ""; }
        }
    }
}
