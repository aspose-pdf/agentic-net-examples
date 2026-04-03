using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths for source PDF, target PDF and temporary XML file
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string tempXmlPath   = "formData.xml";

        // Verify source and target files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }

        try
        {
            // ---------- Export form data from source PDF to XML ----------
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // Save the entire document (including form fields) as XML
                sourceDoc.SaveXml(tempXmlPath);
                Console.WriteLine($"Form data exported to XML: {tempXmlPath}");
            }

            // ---------- Import XML into target PDF ----------
            using (Document targetDoc = new Document(targetPdfPath))
            {
                // Bind the previously saved XML to the target document.
                // This updates the form fields in the target PDF with the values from the XML.
                targetDoc.BindXml(tempXmlPath);

                // Save the updated target PDF
                targetDoc.Save("target_updated.pdf");
                Console.WriteLine("Target PDF updated with imported form data.");
            }

            // Optional: clean up temporary XML file
            if (File.Exists(tempXmlPath))
            {
                File.Delete(tempXmlPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}