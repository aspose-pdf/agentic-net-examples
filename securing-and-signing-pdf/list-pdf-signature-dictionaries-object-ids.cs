using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document contains a form (AcroForm)
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over all form fields and select the signature fields
            foreach (Field field in doc.Form)
            {
                if (field is SignatureField sigField)
                {
                    // The signature dictionary is accessible via the Signature property
                    var signature = sigField.Signature;

                    // Aspose.Pdf does not expose the raw object ID directly.
                    // As a workaround, we can retrieve the indirect reference ID
                    // using the internal GetObjectId method via reflection.
                    // This yields the object number (e.g., "12 0 R").
                    string objectId = GetIndirectObjectId(signature);

                    Console.WriteLine($"Signature Field: {sigField.PartialName}");
                    Console.WriteLine($"  Object ID: {objectId}");
                }
            }
        }
    }

    // Helper method to obtain the indirect object ID of a PDF object via reflection.
    // Returns a string like "12 0 R" or "N/A" if the ID cannot be determined.
    static string GetIndirectObjectId(object pdfObject)
    {
        if (pdfObject == null) return "N/A";

        try
        {
            // The underlying type for PDF objects derives from Aspose.Pdf.Internal.IndirectObject.
            // The field "_objectId" holds the object identifier.
            var type = pdfObject.GetType();
            var fieldInfo = type.GetField("_objectId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (fieldInfo != null)
            {
                var objId = fieldInfo.GetValue(pdfObject);
                return objId?.ToString() ?? "N/A";
            }

            // Some objects expose a public property "ObjectId".
            var propInfo = type.GetProperty("ObjectId", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (propInfo != null)
            {
                var objId = propInfo.GetValue(pdfObject);
                return objId?.ToString() ?? "N/A";
            }
        }
        catch
        {
            // Swallow any reflection errors – we simply return "N/A".
        }

        return "N/A";
    }
}