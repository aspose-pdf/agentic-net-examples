using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            bool anySignature = false;
            Console.WriteLine("Signature dictionaries and their object IDs:");

            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    anySignature = true;

                    // The underlying COS dictionary is not exposed publicly in the current API version.
                    // Use reflection to obtain it and then read its ObjectId property.
                    object dictObj = sigField.GetType()
                                          .GetProperty("Dictionary", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                          ?.GetValue(sigField);

                    int objectId = -1;
                    if (dictObj != null)
                    {
                        PropertyInfo objIdProp = dictObj.GetType().GetProperty("ObjectId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        if (objIdProp != null && objIdProp.PropertyType == typeof(int))
                        {
                            objectId = (int)objIdProp.GetValue(dictObj);
                        }
                    }

                    Console.WriteLine($"- Field \"{sigField.PartialName}\": Object ID = {objectId}");
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No signature fields found in the document.");
            }
        }
    }
}
