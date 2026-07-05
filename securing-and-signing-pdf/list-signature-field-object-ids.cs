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
            // Ensure the document actually contains a form
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("The document does not contain any form fields.");
                return;
            }

            bool anySignature = false;
            // Iterate over all form fields and pick the signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    anySignature = true;
                    // The low‑level COS dictionary that holds the object identifier is not exposed publicly.
                    // We obtain it via reflection (the internal field is named "dictionary" in Aspose.Pdf).
                    var dictFieldInfo = typeof(SignatureField).GetField("dictionary", BindingFlags.NonPublic | BindingFlags.Instance)
                                         ?? typeof(SignatureField).GetField("_dictionary", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (dictFieldInfo != null)
                    {
                        var dict = dictFieldInfo.GetValue(sigField);
                        if (dict != null)
                        {
                            // The COS dictionary type exposes an "ObjectId" property.
                            var objIdProp = dict.GetType().GetProperty("ObjectId", BindingFlags.Public | BindingFlags.Instance);
                            var objId = objIdProp?.GetValue(dict, null);
                            if (objId != null)
                            {
                                Console.WriteLine($"Signature field '{sigField.PartialName}' – Object ID: {objId}");
                            }
                            else
                            {
                                Console.WriteLine($"Signature field '{sigField.PartialName}' – Object ID not available.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Signature field '{sigField.PartialName}' – Dictionary is null.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Signature field '{sigField.PartialName}' – Unable to locate internal dictionary via reflection.");
                    }
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No signature fields found in the document.");
            }
        }
    }
}
