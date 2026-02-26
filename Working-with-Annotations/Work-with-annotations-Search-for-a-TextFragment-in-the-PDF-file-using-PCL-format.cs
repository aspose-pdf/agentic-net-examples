using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPclPath  = "input.pcl";          // source PCL file
        const string outputPdfPath = "output.pdf";         // resulting PDF with annotation
        const string searchPhrase  = "Sample Text";        // text to search for

        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"File not found: {inputPclPath}");
            return;
        }

        try
        {
            // Load the PCL file as a PDF document using PclLoadOptions
            PclLoadOptions loadOptions = new PclLoadOptions();
            using (Document doc = new Document(inputPclPath, loadOptions))
            {
                // Search for the specified phrase in the whole document
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);
                doc.Pages.Accept(absorber); // search all pages

                // If any occurrences are found, add a text annotation to each one
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // The fragment provides its bounding rectangle
                    Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                    // Create a text annotation that points to the found text
                    TextAnnotation annotation = new TextAnnotation(doc.Pages[fragment.Page.Number], rect)
                    {
                        Title    = "Search Result",
                        Contents = $"Found \"{searchPhrase}\"",
                        Open     = true,
                        Icon     = TextIcon.Comment
                    };

                    // Add the annotation to the page
                    doc.Pages[fragment.Page.Number].Annotations.Add(annotation);
                }

                // Save the modified document as PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Search completed. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}