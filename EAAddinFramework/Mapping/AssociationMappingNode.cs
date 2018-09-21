﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MP = MappingFramework;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EAAddinFramework.Mapping
{
    public class AssociationMappingNode : MappingNode
    {
        public AssociationMappingNode(TSF_EA.Association sourceAssociation) : this(sourceAssociation, null) { }
        public AssociationMappingNode(TSF_EA.Association sourceAssociation, ClassifierMappingNode parent) : base(sourceAssociation, parent) { }

        internal TSF_EA.Association sourceAssociation
        {
            get
            {
                return this.source as TSF_EA.Association;
            }
            set
            {
                this.source = value;
            }
        }

        public override IEnumerable<MP.Mapping> getMappings(MP.MappingNode targetRootNode)
        {
            //TODO
            throw new NotImplementedException();
        }

        protected override void setChildNodes()
        {
            //create mapping node for target element
            var targetElement = this.sourceAssociation.targetElement as TSF_EA.ElementWrapper;
            if (targetElement != null)
            {
                var childNode = new ClassifierMappingNode(targetElement, this);
            }
        }
    }
}