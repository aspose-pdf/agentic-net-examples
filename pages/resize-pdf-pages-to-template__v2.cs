using System;
using System.IO;
using Aspose.Pdf;

namespace ResizePagesExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input PDF, template PDF and output PDF paths (relative to the executable folder)
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string sourcePath = Path.Combine(baseDir, "source.pdf");
            string templatePath = Path.Combine(baseDir, "template.pdf");
            string outputPath = Path.Combine(baseDir, "output.pdf");

            // Verify that the required files exist before proceeding
            if (!File.Exists(sourcePath))
            {
                Console.Error.WriteLine($"Error: Source PDF not found at '{sourcePath}'.");
                return;
            }
            if (!File.Exists(templatePath))
            {
                Console.Error.WriteLine($"Error: Template PDF not found at '{templatePath}'.");
                return;
            }

            // Load the source PDF document
            using (Document sourceDoc = new Document(sourcePath))
            {
                // Load the template PDF to obtain the target page size
                using (Document templateDoc = new Document(templatePath))
                {
                    // Use the first page of the template as the reference size
                    double targetWidth = templateDoc.Pages[1].PageInfo.Width;
                    double targetHeight = templateDoc.Pages[1].PageInfo.Height;

                    // Resize every page in the source document to the reference size using PageInfo
                    foreach (Page page in sourceDoc.Pages)
                    {
                        page.PageInfo.Width = targetWidth;
                        page.PageInfo.Height = targetHeight;
                    }
                }

                // Save the resized document
                sourceDoc.Save(outputPath);
                Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
            }
        }
    }
}
