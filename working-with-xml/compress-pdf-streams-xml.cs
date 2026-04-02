using Aspose.Pdf;
using Aspose.Pdf.Optimization;
using System;
using System.IO;

public class Program
{
    public static void Main()
    {
        // Sample XML content to be converted into PDF
        string xmlString = "<root><p>Hello World from XML!</p></root>";

        // Load the XML into a memory stream
        using (MemoryStream xmlStream = new MemoryStream())
        {
            using (StreamWriter writer = new StreamWriter(xmlStream))
            {
                writer.Write(xmlString);
                writer.Flush();
                xmlStream.Position = 0;

                // Create a new PDF document and bind the XML
                using (Document pdfDoc = new Document())
                {
                    pdfDoc.BindXml(xmlStream);

                    // Configure optimization to compress PDF objects
                    OptimizationOptions opt = new OptimizationOptions();
                    opt.CompressObjects = true;

                    // Apply the optimization settings
                    pdfDoc.OptimizeResources(opt);

                    // Save the compressed PDF
                    pdfDoc.Save("compressed.pdf");
                }
            }
        }
    }
}