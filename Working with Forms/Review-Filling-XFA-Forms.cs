using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Forms;               // Access to XFA via Document.Form.XFA
using FacadeForm = Aspose.Pdf.Facades.Form; // Alias to avoid ambiguity with Aspose.Pdf.Forms.Form

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // Source PDF containing an XFA form
        const string outputPdf = "filled.pdf";     // Resulting PDF after filling
        const string xfaExport = "xfaData.xml";    // Optional export of the XFA packet

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Check whether the document actually contains an XFA form
            if (!doc.Form.HasXfa)
            {
                Console.WriteLine("The PDF does not contain an XFA form.");
            }
            else
            {
                // List all XFA field names (useful for debugging / discovery)
                Console.WriteLine("XFA fields found:");
                foreach (string fieldName in doc.Form.XFA.FieldNames)
                {
                    Console.WriteLine($"  - {fieldName}");
                }

                // Example: assign values to specific XFA fields using the indexer
                // (Replace the field names with those that exist in your template)
                doc.Form.XFA["FirstName"] = "John";
                doc.Form.XFA["LastName"]  = "Doe";
                doc.Form.XFA["Address"]   = "123 Main St, Anytown";

                // OPTIONAL: replace the whole XFA data packet with a new XML document
                // XmlDocument newXfa = new XmlDocument();
                // newXfa.Load("newXfaTemplate.xml");
                // doc.Form.AssignXfa(newXfa);
            }

            // OPTIONAL: extract the raw XFA XML packet using the Facade API
            // This demonstrates how to work with the Form facade without ambiguity.
            FacadeForm formFacade = new FacadeForm(doc);
            using (FileStream xfaStream = new FileStream(xfaExport, FileMode.Create, FileAccess.Write))
            {
                formFacade.ExtractXfaData(xfaStream);
            }
            // No explicit Close() needed; the facade does not hold unmanaged resources beyond the Document.

            // Persist the changes to a new PDF file
            doc.Save(outputPdf);
        }

        Console.WriteLine($"XFA form processing completed. Output saved to '{outputPdf}'.");
    }
}