using System;
using System.IO;
using System.Linq;
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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve all signature fields via the generic Fields collection
                var signatureFields = doc?.Form?.Fields?.OfType<SignatureField>();

                if (signatureFields == null || !signatureFields.Any())
                {
                    Console.WriteLine("No signature dictionaries found in the document.");
                }
                else
                {
                    Console.WriteLine("Signature dictionaries (object IDs):");
                    int index = 1;
                    foreach (var sigField in signatureFields)
                    {
                        // The ObjectId is not exposed publicly. Use reflection to obtain the underlying dictionary
                        int objectId = GetObjectIdViaReflection(sigField);
                        Console.WriteLine($"Signature {index}: Object ID {objectId}");
                        index++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Uses reflection to fetch the internal PDF dictionary of a SignatureField and read its ObjectId.
    /// Returns -1 if the ObjectId cannot be determined.
    /// </summary>
    private static int GetObjectIdViaReflection(SignatureField sigField)
    {
        if (sigField == null)
            return -1;

        // SignatureField may expose a non‑public "Dictionary" property that holds the low‑level PDF dictionary.
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        PropertyInfo dictProp = sigField.GetType().GetProperty("Dictionary", flags);
        if (dictProp == null)
            return -1;

        object dictObj = dictProp.GetValue(sigField);
        if (dictObj == null)
            return -1;

        // The dictionary object (CosPdfDictionary / DictionaryEditor) contains an "ObjectId" property.
        PropertyInfo idProp = dictObj.GetType().GetProperty("ObjectId", flags);
        if (idProp == null)
            return -1;

        object idValue = idProp.GetValue(dictObj);
        if (idValue is int id)
            return id;

        // Some versions expose ObjectId as a long.
        if (idValue is long longId)
            return (int)longId;

        return -1;
    }
}
