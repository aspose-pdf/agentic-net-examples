using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Required for Form facade

class Program
{
    static void Main(string[] args)
    {
        // Paths to the source PDF form and the XML data file.
        // Adjust these paths as needed or obtain them from command‑line arguments.
        string pdfPath = "form.pdf";
        string xmlPath = "data.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the PDF document. Using a Document instance is the recommended way.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Create the Form facade bound to the loaded PDF document.
            using (Form formFacade = new Form(pdfDoc))
            {
                // Import the XML data into the PDF form.
                using (FileStream xmlStream = File.OpenRead(xmlPath))
                {
                    formFacade.ImportXml(xmlStream);
                }

                // Preserve the original XML filename and append the .fdf suffix.
                // Example: "data.xml" -> "data.fdf"
                string fdfPath = Path.ChangeExtension(xmlPath, ".fdf");

                // Export the form data to an FDF file.
                using (FileStream fdfStream = File.Create(fdfPath))
                {
                    formFacade.ExportFdf(fdfStream);
                }

                Console.WriteLine($"FDF file created: {fdfPath}");
            }
        }
    }
}
