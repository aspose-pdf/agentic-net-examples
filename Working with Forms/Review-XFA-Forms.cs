using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string extractedXfaXml = "extracted_xfa.xml";
        const string newXfaXml = "new_xfa.xml";
        const string xfdfFile = "data.xfdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document to inspect its form.
        using (Document doc = new Document(inputPdf))
        {
            // Determine whether the document contains an XFA form.
            bool hasXfa = doc.Form.HasXfa;
            Console.WriteLine($"Has XFA: {hasXfa}");

            if (hasXfa)
            {
                // Extract the XFA data packet to an XML file using the Form facade.
                using (FileStream xfaOut = new FileStream(extractedXfaXml, FileMode.Create, FileAccess.Write))
                using (Form formFacade = new Form(inputPdf))
                {
                    formFacade.ExtractXfaData(xfaOut);
                }
                Console.WriteLine($"XFA data extracted to '{extractedXfaXml}'.");

                // Optionally replace the XFA data with a new XML document.
                if (File.Exists(newXfaXml))
                {
                    XmlDocument newXfaDoc = new XmlDocument();
                    newXfaDoc.Load(newXfaXml);
                    // Assign the new XFA data to the document's form.
                    doc.Form.AssignXfa(newXfaDoc);
                    Console.WriteLine("New XFA data assigned to the document.");
                }
            }
        }

        // Import XFDF field values into the PDF (if an XFDF file is present).
        if (File.Exists(xfdfFile))
        {
            using (Form formFacade = new Form(inputPdf))
            using (FileStream xfdfIn = new FileStream(xfdfFile, FileMode.Open, FileAccess.Read))
            {
                formFacade.ImportXfdf(xfdfIn);
                // Save the modified PDF to a new file.
                formFacade.Save(outputPdf);
            }
            Console.WriteLine($"XFDF data imported and saved to '{outputPdf}'.");
        }
        else
        {
            Console.WriteLine("XFDF file not found; import step skipped.");
        }
    }
}