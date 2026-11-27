using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string fdfPath = "output.fdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML data from a stream to keep the original Unicode encoding intact
        using (FileStream xmlStream = File.OpenRead(xmlPath))
        {
            // The Form facade works on a PDF document, so create (or load) a document and bind it.
            // An empty document is sufficient when you only need to convert XML → FDF.
            using (Document pdfDoc = new Document())
            {
                // Bind the document to the Form facade. Form does NOT implement IDisposable, so do NOT use a using block for it.
                Form formFacade = new Form(pdfDoc);

                // Import the XML data (Unicode characters are preserved because we use the raw stream).
                formFacade.ImportXml(xmlStream);

                // Export the data to FDF.
                using (FileStream fdfStream = File.Create(fdfPath))
                {
                    formFacade.ExportFdf(fdfStream);
                }
            }
        }

        Console.WriteLine($"XML to FDF conversion completed: {fdfPath}");
    }
}
