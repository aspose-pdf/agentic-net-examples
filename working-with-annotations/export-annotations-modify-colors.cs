using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_modified.pdf";
        const string tempXfdfPath   = "annotations.xfdf";
        const string modifiedXfdfPath = "annotations_modified.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Export existing annotations to XFDF
                pdfDoc.ExportAnnotationsToXfdf(tempXfdfPath);
            }

            // Load the exported XFDF XML
            XmlDocument xfdfXml = new XmlDocument();
            xfdfXml.Load(tempXfdfPath);

            // Change all annotation colors to green (RGB = 0,255,0)
            // XFDF stores colors as hexadecimal strings without the leading '#'
            XmlNodeList colorNodes = xfdfXml.SelectNodes("//color");
            foreach (XmlNode colorNode in colorNodes)
            {
                // Set to green (00FF00)
                colorNode.InnerText = "00FF00";
            }

            // Save the modified XFDF to a new file
            xfdfXml.Save(modifiedXfdfPath);

            // Re‑open the original PDF and import the modified annotations
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                pdfDoc.ImportAnnotationsFromXfdf(modifiedXfdfPath);
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations exported, colors changed, and PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary files (optional)
            try { if (File.Exists(tempXfdfPath)) File.Delete(tempXfdfPath); } catch { }
            try { if (File.Exists(modifiedXfdfPath)) File.Delete(modifiedXfdfPath); } catch { }
        }
    }
}