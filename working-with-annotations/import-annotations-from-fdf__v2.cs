using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "annotations.fdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Simple FDF parser – expects lines in the form:
            // Page=1;Text=Sample annotation;X=100;Y=500;Width=200;Height=50
            foreach (string rawLine in File.ReadAllLines(fdfPath))
            {
                if (string.IsNullOrWhiteSpace(rawLine))
                    continue;

                // Split the line into key/value pairs
                var parts = rawLine.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                int pageNumber = 0;
                string text = string.Empty;
                double x = 100, y = 500, width = 200, height = 50; // defaults

                foreach (string part in parts)
                {
                    var kv = part.Split(new[] { '=' }, 2);
                    if (kv.Length != 2)
                        continue;

                    string key = kv[0].Trim().ToLowerInvariant();
                    string value = kv[1].Trim();

                    switch (key)
                    {
                        case "page":
                            int.TryParse(value, out pageNumber);
                            break;
                        case "text":
                            text = value;
                            break;
                        case "x":
                            double.TryParse(value, out x);
                            break;
                        case "y":
                            double.TryParse(value, out y);
                            break;
                        case "width":
                            double.TryParse(value, out width);
                            break;
                        case "height":
                            double.TryParse(value, out height);
                            break;
                    }
                }

                // Validate page number
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} in FDF line: {rawLine}");
                    continue;
                }

                // Create a rectangle for the annotation (lower‑left X,Y, upper‑right X+W,Y+H)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    x,
                    y,
                    x + width,
                    y + height);

                // Create a text annotation and set its properties
                TextAnnotation annotation = new TextAnnotation(doc.Pages[pageNumber], rect)
                {
                    Title = "FDF Import",
                    Contents = text,
                    Open = true,
                    Icon = TextIcon.Note
                };

                // Add the annotation to the appropriate page
                doc.Pages[pageNumber].Annotations.Add(annotation);
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported from '{fdfPath}' and saved to '{outputPath}'.");
    }
}