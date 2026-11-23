using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "styled_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Locate the first visual table using TableAbsorber
            TableAbsorber absorber = new TableAbsorber();
            foreach (Page page in doc.Pages)
            {
                absorber.Visit(page);
                if (absorber.TableList.Count > 0)
                    break; // stop after the first table is found
            }

            if (absorber.TableList.Count == 0)
            {
                Console.Error.WriteLine("No table found in the PDF.");
                return;
            }

            // The first absorbed table
            AbsorbedTable absorbedTable = absorber.TableList[0];

            // Get the page that contains the table and its bounding rectangle
            Page tablePage = doc.Pages[absorbedTable.PageNum];
            Aspose.Pdf.Rectangle tableRect = absorbedTable.Rectangle;

            // Create a transparent square annotation that matches the table bounds
            // and give it a visible border (1‑point black)
            var borderAnnotation = new SquareAnnotation(tablePage, tableRect)
            {
                // Fill should be transparent so we only see the border
                Color = Aspose.Pdf.Color.Transparent,
                InteriorColor = Aspose.Pdf.Color.Transparent
            };
            // Border requires the parent annotation in its constructor
            borderAnnotation.Border = new Border(borderAnnotation) { Width = 1 };
            // The border colour is taken from the annotation's own Color property
            borderAnnotation.Color = Aspose.Pdf.Color.Black;

            // Add the annotation to the page
            tablePage.Annotations.Add(borderAnnotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with styled table border to '{outputPath}'.");
    }
}
