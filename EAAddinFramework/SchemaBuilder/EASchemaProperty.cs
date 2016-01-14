﻿
using System;
using System.Collections.Generic;
using SBF=SchemaBuilderFramework;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EAAddinFramework.SchemaBuilder
{
	/// <summary>
	/// Description of EASchemaProperty.
	/// </summary>
	public class EASchemaProperty: EASchemaPropertyWrapper, SBF.SchemaProperty
	{
		/// <summary>
		/// constructor. Nothing specific, just calling base constructor
		/// </summary>
		/// <param name="model">the model</param>
		/// <param name="owner">the owner Schema Element</param>
		/// <param name="objectToWrap">the EA.SchemaProperty object to wrap</param>
		public EASchemaProperty(UTF_EA.Model model,EASchemaElement owner, EA.SchemaProperty objectToWrap):base(model,owner, objectToWrap){}
		
		/// <summary>
		/// The property int he model where this Schema property was derived from
		/// </summary>
		public UML.Classes.Kernel.Property sourceProperty 
		{
			get 
			{
				return this.model.getAttributeByGUID(this.wrappedProperty.GUID);
			}
			set 
			{
				throw new NotImplementedException();
			}
		}
		/// <summary>
		/// the property in the subset model generated from this property
		/// </summary>
		public UML.Classes.Kernel.Property subSetProperty {get;set;}

		/// <summary>
		/// Creates the subset property in the subsetElement of the owning SchemaElement
		/// </summary>
		/// <returns>the new property</returns>
		public UML.Classes.Kernel.Property createSubsetProperty()
		{
			this.subSetProperty = this.model.factory.createNewElement<UML.Classes.Kernel.Property>(this.owner.subsetElement,this.sourceProperty.name);
			this.subSetProperty.type = this.sourceProperty.type;
			this.subSetProperty.stereotypes = this.sourceProperty.stereotypes;
			this.subSetProperty.lower = this.sourceProperty.lower;
			this.subSetProperty.upper = this.sourceProperty.upper;
			((UTF_EA.Element) this.subSetProperty).save();
			//copy tagged values
			((UTF_EA.Element) this.subSetProperty).copyTaggedValues((UTF_EA.Element)this.sourceProperty);
			//add tagged value with reference to source association
			((UTF_EA.Element)this.subSetProperty).addTaggedValue(EASchemaBuilderFactory.sourceAttributeTagName,((UTF_EA.Element)this.sourceProperty).guid);
			return this.subSetProperty;
		}
		/// <summary>
		/// Checks if the attribute type is present as the source element of one of the schema elements
		/// If it finds a match the type is set to the subset elemnt of this schema element
		/// </summary>
		/// <param name="schemaElements">the list of schema elements in the schema</param>
		public void resolveAttributeType(HashSet<SBF.SchemaElement> schemaElements)
		{
			foreach (EASchemaElement element in schemaElements) 
			{
				if (element.sourceElement != null && 
				    element.subsetElement != null &&
				    this.subSetProperty != null &&
				    element.sourceElement.Equals(this.subSetProperty.type))
				{
					//replace the type if it matches the source element
					this.subSetProperty.type = element.subsetElement;
					((UTF_EA.Attribute)this.subSetProperty).save();
				}
			}
		}
	}
}
