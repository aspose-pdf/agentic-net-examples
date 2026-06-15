using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "form.pdf";          // source PDF with form fields
        const string filledPdf  = "form_filled.pdf";   // PDF after filling fields
        const string exportXml  = "form_data.xml";     // exported XML with UTF‑8 encoding
        const string importedPdf = "form_imported.pdf"; // PDF after importing XML

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // Fill form fields with Unicode values
        // ------------------------------------------------------------
        using (Form formFiller = new Form(inputPdf, filledPdf))
        {
            // Example Unicode strings
            formFiller.FillField("FirstName", "José 🚀");
            formFiller.FillField("LastName",  "Иванов");
            formFiller.FillField("Comments", "こんにちは世界"); // Japanese

            // Persist the filled PDF
            formFiller.Save();
        }

        // ------------------------------------------------------------
        // Export form data to XML using UTF‑8 encoding
        // ------------------------------------------------------------
        using (Form formExporter = new Form(filledPdf))
        {
            using (MemoryStream tempStream = new MemoryStream())
            {
                // Export XML to a memory stream (binary UTF‑8 data)
                formExporter.ExportXml(tempStream);
                tempStream.Position = 0; // rewind for reading

                // Read the XML as a UTF‑8 string
                using (StreamReader reader = new StreamReader(tempStream, Encoding.UTF8))
                {
                    string xmlContent = reader.ReadToEnd();

                    // Write the XML to a file explicitly with UTF‑8 encoding
                    using (StreamWriter writer = new StreamWriter(exportXml, false, Encoding.UTF8))
                    {
                        writer.Write(xmlContent);
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // Import the UTF‑8 XML back into a new PDF
        // ------------------------------------------------------------
        using (Form formImporter = new Form(filledPdf, importedPdf))
        {
            // Open the XML file with a FileStream (UTF‑8 content)
            using (FileStream xmlStream = new FileStream(exportXml, FileMode.Open, FileAccess.Read))
            {
                formImporter.ImportXml(xmlStream);
            }

            // Save the PDF that now contains the imported Unicode values
            formImporter.Save();
        }

        Console.WriteLine("Form processing with Unicode support completed.");
    }
}