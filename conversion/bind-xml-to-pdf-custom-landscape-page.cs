using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath   = "data.xml";
        const string outputPdf = "result.pdf";

        // Verify the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load an empty PDF document, bind the XML data, set custom page size, and save
        using (Document doc = new Document())
        {
            // Bind the XML data to the document (fills form fields, etc.)
            doc.BindXml(xmlPath);

            // Define custom page dimensions and landscape orientation
            PageInfo pageInfo = new PageInfo
            {
                IsLandscape = true,
                // Example dimensions in points (1 point = 1/72 inch)
                // Adjust Width and Height to the desired size
                Width  = 842, // Landscape A4 width
                Height = 595  // Landscape A4 height
            };
            doc.PageInfo = pageInfo;

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated and saved to '{outputPdf}'.");
    }
}