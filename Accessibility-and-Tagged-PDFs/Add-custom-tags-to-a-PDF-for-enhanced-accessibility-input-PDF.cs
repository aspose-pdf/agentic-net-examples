using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Facades; // Included as per requirement

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (write‑only setters)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // -------------------------------------------------
            // Add a custom paragraph element
            // -------------------------------------------------
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This PDF has been enhanced with custom tags for accessibility.");
            root.AppendChild(paragraph); // AppendChild with a single argument

            // -------------------------------------------------
            // Add a figure element with alternative text
            // -------------------------------------------------
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Diagram illustrating the process flow.";
            // If you have an image to associate, uncomment the line below and provide a valid path
            // figure.SetImage("diagram.png");
            root.AppendChild(figure);

            // -------------------------------------------------
            // Add a table element with header and body rows
            // -------------------------------------------------
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table.";
            root.AppendChild(table);

            // Header section
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);
            TableTHElement thItem = tagged.CreateTableTHElement();
            thItem.SetText("Item");
            headerRow.AppendChild(thItem);
            TableTHElement thQty = tagged.CreateTableTHElement();
            thQty.SetText("Quantity");
            headerRow.AppendChild(thQty);

            // Body section
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);
            TableTDElement tdApples = tagged.CreateTableTDElement();
            tdApples.SetText("Apples");
            bodyRow.AppendChild(tdApples);
            TableTDElement tdCount = tagged.CreateTableTDElement();
            tdCount.SetText("10");
            bodyRow.AppendChild(tdCount);

            // -------------------------------------------------
            // Convert to PDF/A while preserving tags (optional)
            // -------------------------------------------------
            // PdfFormatConversionOptions requires a constructor with a PdfFormat argument
            PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
            // Enable auto‑tagging during conversion if desired
            conversionOptions.AutoTaggingSettings.EnableAutoTagging = true;
            doc.Convert(conversionOptions);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}