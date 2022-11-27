﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Magma.LuaHelper
{
    public class XsdParser
    {
        public static List<XsdElementInformation> AnalyseSchema(XmlSchemaSet set)
        {
            List<XsdElementInformation> flatList = new List<XsdElementInformation>();
            // Retrieve the compiled XmlSchema object from the XmlSchemaSet
            // by iterating over the Schemas property.
            XmlSchema customerSchema = null;
            foreach (XmlSchema schema in set.Schemas())
            {
                customerSchema = schema;
            }

            // Iterate over each XmlSchemaElement in the Values collection
            // of the Elements property.
            foreach (XmlSchemaElement element in customerSchema.Elements.Values)
            {
                RecursiveElementAnalyser("//", element, flatList);
            }
            return flatList;
        }

        public static XsdElementInformation RecursiveElementAnalyser(string prefix, XmlSchemaElement element, List<XsdElementInformation> flatList)
        {
            XsdElementInformation xsdElementInformation = new XsdElementInformation();

            if (prefix == "//")
            {
                xsdElementInformation.IsRoot = true;
            }

            string elementName = prefix + element.Name;

            string dataType = element.ElementSchemaType.TypeCode.ToString();
            xsdElementInformation.Name = element.Name;
            xsdElementInformation.DataType = dataType;
            xsdElementInformation.XPathLikeKey = elementName;

            // Get the complex type of the Customer element.
            XmlSchemaComplexType complexType = element.ElementSchemaType as XmlSchemaComplexType;

            if (complexType != null)
            {
                // If the complex type has any attributes, get an enumerator 
                // and write each attribute name to the console.
                if (complexType.AttributeUses.Count > 0)
                {
                    IDictionaryEnumerator enumerator =
                        complexType.AttributeUses.GetEnumerator();

                    while (enumerator.MoveNext())
                    {
                        XmlSchemaAttribute attribute =
                            (XmlSchemaAttribute)enumerator.Value;

                        string attrDataType = attribute.AttributeSchemaType.TypeCode.ToString();

                        xsdElementInformation.Attributes.Add(new XsdAttributeInformation() { Name = attribute.Name, DataType = attrDataType });
                    }
                }

                // Get the sequence particle of the complex type.
                XmlSchemaSequence sequence = complexType.ContentTypeParticle as XmlSchemaSequence;

                if (sequence != null)
                {
                    // Iterate over each XmlSchemaElement in the Items collection.
                    foreach (var childElement in sequence.Items)
                    {
                        var xmlSchemaElement = childElement as XmlSchemaElement;
                        if (xmlSchemaElement != null)
                        {
                            var result = RecursiveElementAnalyser(xsdElementInformation.XPathLikeKey + "/",
                                xmlSchemaElement, flatList);
                            xsdElementInformation.Elements.Add(result);
                        }
                        else
                        {
                            // support for XmlSchemaChoise element list
                            var choice = childElement as XmlSchemaChoice;
                            if (choice != null)
                            {
                                foreach (var choiceElement in choice.Items)
                                {
                                    var xmlChoiceSchemaElement = choiceElement as XmlSchemaElement;
                                    if (xmlChoiceSchemaElement != null)
                                    {
                                        var result = RecursiveElementAnalyser(xsdElementInformation.XPathLikeKey + "/",
                                            xmlChoiceSchemaElement, flatList);
                                        xsdElementInformation.Elements.Add(result);
                                    }
                                }
                            }
                        }
                    }
                }


            }

            flatList.Add(xsdElementInformation);

            return xsdElementInformation;
        }
    }
}
