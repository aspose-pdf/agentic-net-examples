using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "SampleData.pdf";
        const string xmlPath   = "DataExport.xml";
        const string outputPdf = "RoundTripXml.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Bind the document to the Form facade
                using (Aspose.Pdf.Facades.Form formFacade = new Aspose.Pdf.Facades.Form(doc))
                {
                    // Export current form field values to XML (stream‑based API)
                    using (FileStream exportStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                    {
                        formFacade.ExportXml(exportStream);
                    }
                    Console.WriteLine($"Form data exported to '{xmlPath}'.");

                    // Re‑import the same XML data back into the form
                    using (FileStream importStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                    {
                        formFacade.ImportXml(importStream);
                    }
                    Console.WriteLine($"Form data imported from '{xmlPath}'.");

                    // Save the PDF after the round‑trip operation
                    formFacade.Save(outputPdf);
                    Console.WriteLine($"Round‑trip PDF saved as '{outputPdf}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
