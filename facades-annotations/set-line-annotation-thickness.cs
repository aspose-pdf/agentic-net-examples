using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_thick_line.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, locate the first LineAnnotation and change its thickness.
        using (Document doc = new Document(inputPath))
        {
            bool lineFound = false;

            foreach (Page page in doc.Pages)
            {
                for (int i = 0; i < page.Annotations.Count; i++)
                {
                    if (page.Annotations[i] is LineAnnotation lineAnnot)
                    {
                        // Set the border width (thickness) to 3 points.
                        lineAnnot.Border = new Border(lineAnnot) { Width = 3 };
                        lineFound = true;
                        break; // stop searching – we only need the first line.
                    }
                }
                if (lineFound) break;
            }

            if (!lineFound)
            {
                Console.Error.WriteLine("No Line annotation found in the document.");
                return;
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation thickness updated and saved to '{outputPath}'.");
    }
}
