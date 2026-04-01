using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // ImageStamp, alignment enums

class Program
{
    static void Main()
    {
        string inputDirectory = "input";
        string logoPath = "logo.png";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine("Input directory not found: " + inputDirectory);
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine("Logo file not found: " + logoPath);
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        foreach (string pdfFile in pdfFiles)
        {
            // Fully qualify System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
            string outputFileName = System.IO.Path.GetFileNameWithoutExtension(pdfFile) + "_logo.pdf";

            using (Document doc = new Document(pdfFile))
            {
                // ImageStamp lives in Aspose.Pdf.Drawing namespace
                ImageStamp stamp = new ImageStamp(logoPath)
                {
                    // IsBackground property does not exist in recent Aspose.Pdf versions; default is false.
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Top,
                    XIndent = 10f,
                    YIndent = 10f
                };

                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(stamp);
                }

                doc.Save(outputFileName);
            }

            Console.WriteLine("Saved: " + outputFileName);
        }
    }
}