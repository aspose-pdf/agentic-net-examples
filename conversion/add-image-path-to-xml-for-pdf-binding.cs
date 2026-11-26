using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath      = "template.pdf";      // PDF with data‑binding placeholders
        const string xmlPath      = "data.xml";          // Original XML data file
        const string imagePath    = "picture.jpg";       // Image to insert (runtime path)
        const string tempXmlPath  = "data_with_image.xml";
        const string outputPdf    = "output.pdf";

        // Verify files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        try
        {
            // Load the original XML
            XDocument xmlDoc = XDocument.Load(xmlPath);

            // Create a new element that will hold the image file path.
            // The element name must match the placeholder used in the PDF template.
            // For example, if the PDF contains a field like ${ImageField},
            // the XML should contain <ImageField>...</ImageField>.
            XElement imageElement = new XElement("ImageField", imagePath);

            // Insert the new element into the root (or appropriate parent).
            // Adjust the location according to your XML schema.
            xmlDoc.Root?.Add(imageElement);

            // Save the modified XML to a temporary file.
            xmlDoc.Save(tempXmlPath);

            // Load the PDF document.
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfPath))
            {
                // Bind the modified XML to the PDF.
                // This replaces placeholders in the PDF with values from the XML,
                // including the image path we just added.
                pdfDoc.BindXml(tempXmlPath);

                // Save the resulting PDF.
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"PDF generated successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary XML file.
            if (File.Exists(tempXmlPath))
            {
                try { File.Delete(tempXmlPath); } catch { /* ignore */ }
            }
        }
    }
}
