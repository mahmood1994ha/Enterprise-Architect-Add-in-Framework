﻿using System;
using System.Collections.Generic;
using System.Linq;

using UML = TSF.UmlToolingFramework.UML;

namespace TSF.UmlToolingFramework.Wrappers.EA
{
    public class Stereotype : Element, UML.Profiles.Stereotype
    {

        
        private Diagram _ownerDiagram;

        public Stereotype(Model model, Element owner, string stereotype)
          : base(model)
        {
            this.name = stereotype;
            this.owner = owner;
        }
        public Stereotype(Model model, Diagram ownerDiagram, string stereotype) : this(model, null as Element, stereotype)
        {
            this._ownerDiagram = ownerDiagram;
        }
        private string _name;
        public override string name
        {
            get => this._name;
            set 
            {
                if (value.Contains("::"))
                {
                    var parts = value.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
                    this._profileName = parts[0];
                    this._name = parts[1];
                }
                else
                {
                    this._name = value;
                }
            }
                
        }
        private string _profileName = string.Empty;
        public string profileName
        {
            get => this._profileName;
            set => this._profileName = value;   
        }
        public UML.Classes.Kernel.VisibilityKind visibility
        {
            get { return this.owningNamespace.visibility; }
            set { }
        }

        public string qualifiedName
        {
            get => string.IsNullOrEmpty(this.profileName) ?
                   this.name :
                   string.Join("::", new string[] { this.profileName, this.name });
            set => this.name = value;
        }

        /// if the owning element is a namespace then this is returned.
        /// If not then the owning namespace of the owner is returned
        public UML.Classes.Kernel.Namespace owningNamespace
        {
            get { return this.getOwner<UML.Classes.Kernel.Namespace>(); }
            set { this.owner = value; }
        }

        public override UML.Classes.Kernel.Element owner
        {
            get { return this._owner; }
            set { this._owner = value as Element; }
        }

        public override HashSet<UML.Classes.Kernel.Element> ownedElements
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override HashSet<UML.Profiles.Stereotype> stereotypes
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public List<UML.Classes.Dependencies.Dependency> clientDependencies
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public List<UML.Classes.Dependencies.Dependency> supplierDependencies
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        internal override void saveElement()
        {
            //nothing to save here as it is save together with the owning element
        }

        public override string notes
        {
            get { return string.Empty; }
            set { /* do nothing, stereotypes do not have comments in EA */ }
        }

        public override TSF.UmlToolingFramework.UML.Extended.UMLItem getItemFromRelativePath(List<string> relativePath)
        {
            return null;
        }
        internal static string getStereotypeEx(IEnumerable<UML.Profiles.Stereotype> stereotypes)
        {
            List<string> stereotypeList = new List<string>();
            foreach (Stereotype stereotype in stereotypes)
            {
                stereotypeList.Add(stereotype.name);
            }
            return string.Join(",", stereotypeList);
        }

        internal override global::EA.Collection eaTaggedValuesCollection
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string guid
        {
            get
            {   //no guid's for stereotypes in EA.
                throw new NotImplementedException();
            }
        }


        #region implemented abstract members of Element

        public override void deleteOwnedElement(Element ownedElement)
        {
            throw new NotImplementedException();
        }

        public override bool makeWritable(bool overrideLocks)
        {
            return true;
        }

        public override string getLockedUser()
        {
            return string.Empty;
        }

        public override string getLockedUserID()
        {
            return string.Empty;
        }

        #endregion

        #region implemented abstract members of Element

        protected override string getTaggedValueQuery(string taggedValueName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
