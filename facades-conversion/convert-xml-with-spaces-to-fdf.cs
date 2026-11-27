using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // XML file name contains spaces; use a literal string.
        string xmlFileName = "sample data.xml";
        // Desired FDF output file name (also may contain spaces).
        string fdfFileName = "output data.fdf";

        // Resolve full absolute paths – this avoids any issues with spaces in the path.
        string xmlPath = Path.GetFullPath(xmlFileName);
        string fdfPath = Path.GetFullPath(fdfFileName);

        // Verify that the source XML exists before attempting conversion.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Input XML not found: {xmlPath}");
            return;
        }

        // Create a dummy PDF document (or load an existing template) to bind the form data.
        // For the purpose of this example a new empty document is sufficient.
        using (Document pdfDoc = new Document())
        {
            // The Form facade works with streams, not file paths.
            using (Form formFacade = new Form(pdfDoc))
            {
                // Import the XML data.
                using (FileStream xmlStream = File.OpenRead(xmlPath))
                {
                    formFacade.ImportXml(xmlStream);
                }

                // Export the data as FDF.
                using (FileStream fdfStream = File.Create(fdfPath))
                {
                    formFacade.ExportFdf(fdfStream);
                }
            }
        }

        Console.WriteLine($"Conversion successful. FDF saved to: {fdfPath}");
    }
}
