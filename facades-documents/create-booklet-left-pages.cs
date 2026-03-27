using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfExamples
{
    public class BookletCreator
    {
        public static void CreateBooklet(string inputPath, string outputPath)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Load the source PDF to determine page count
            using (Document sourceDoc = new Document(inputPath))
            {
                int totalPages = sourceDoc.Pages.Count;
                int half = totalPages / 2;

                // Left pages: even numbers from the first half of the document
                List<int> leftList = new List<int>();
                for (int i = 2; i <= half; i += 2)
                {
                    leftList.Add(i);
                }

                // Right pages: odd numbers from the second half of the document
                List<int> rightList = new List<int>();
                for (int i = half + 1; i <= totalPages; i += 2)
                {
                    rightList.Add(i);
                }

                int[] leftPages = leftList.ToArray();
                int[] rightPages = rightList.ToArray();

                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);
                if (success)
                {
                    Console.WriteLine($"Booklet created successfully: {outputPath}");
                }
                else
                {
                    Console.Error.WriteLine("Failed to create booklet.");
                }
            }
        }

        public static void Main()
        {
            const string inputPath = "source.pdf";
            const string outputPath = "booklet.pdf";
            CreateBooklet(inputPath, outputPath);
        }
    }
}
