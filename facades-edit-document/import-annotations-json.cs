using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class AnnotationDefinition
{
    public int Page { get; set; }
    public double Llx { get; set; }
    public double Lly { get; set; }
    public double Urx { get; set; }
    public double Ury { get; set; }
    public string Text { get; set; }
    public string ColorName { get; set; }
}

public class Program
{
    public static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string jsonPath = "annotations.json";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            List<AnnotationDefinition> definitions = JsonSerializer.Deserialize<List<AnnotationDefinition>>(jsonContent);

            // Load the PDF document
            Document pdfDoc = new Document(inputPdfPath);

            foreach (AnnotationDefinition definition in definitions)
            {
                // Create a TextFragment with the desired text
                TextFragment fragment = new TextFragment(definition.Text);

                // Set position – lower‑left corner of the rectangle
                fragment.Position = new Position(definition.Llx, definition.Lly);

                // Configure text state (font, size, color, etc.)
                // TextState is read‑only; modify its members directly
                fragment.TextState.Font = FontRepository.FindFont("Helvetica");
                fragment.TextState.FontSize = 12;

                // Resolve color name to Aspose.Pdf.Color
                if (!string.IsNullOrEmpty(definition.ColorName))
                {
                    if (definition.ColorName.Equals("Red", StringComparison.OrdinalIgnoreCase))
                        fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
                    else if (definition.ColorName.Equals("Blue", StringComparison.OrdinalIgnoreCase))
                        fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                    else if (definition.ColorName.Equals("Green", StringComparison.OrdinalIgnoreCase))
                        fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Green;
                    else
                        fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                }
                else
                {
                    fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                }

                // Ensure the page index is valid (Aspose pages are 1‑based)
                if (definition.Page < 1 || definition.Page > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page number {definition.Page} is out of range. Skipping annotation.");
                    continue;
                }

                // Add the fragment to the specified page
                pdfDoc.Pages[definition.Page].Paragraphs.Add(fragment);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Annotations imported and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
