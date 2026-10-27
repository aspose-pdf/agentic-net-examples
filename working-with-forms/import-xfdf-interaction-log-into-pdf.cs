using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class InteractionReconstructor
{
    static void Main()
    {
        const string pdfPath        = "input.pdf";
        const string xmlLogPath     = "interactions.xml";
        const string outputPdfPath  = "reconstructed_output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlLogPath))
        {
            Console.Error.WriteLine($"XML log not found: {xmlLogPath}");
            return;
        }

        // Load interaction log
        XDocument logDoc = XDocument.Load(xmlLogPath);
        // Load PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over each Interaction element
            foreach (XElement interaction in logDoc.Root.Elements("Interaction"))
            {
                // Parse required attributes
                int pageNumber = (int)interaction.Attribute("Page");
                double x       = (double)interaction.Attribute("X");
                double y       = (double)interaction.Attribute("Y");
                string type    = (string)interaction.Attribute("Type") ?? "Interaction";
                string desc    = (string)interaction.Attribute("Description") ?? $"{type}";

                // Validate page number (Aspose.Pdf uses 1‑based indexing)
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} in log entry; skipping.");
                    continue;
                }

                Page page = pdfDoc.Pages[pageNumber];

                // Define a small rectangle around the point (width=20, height=20)
                // Use fully qualified Rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(x, y, x + 20, y + 20);

                // Create a text annotation showing the interaction description
                TextAnnotation annotation = new TextAnnotation(page, rect)
                {
                    Title    = type,
                    Contents = desc,
                    Open     = true,
                    Icon     = TextIcon.Note,
                    // Use Aspose.Pdf.Color for cross‑platform compatibility
                    Color    = Aspose.Pdf.Color.LightYellow
                };

                // Add the annotation to the page
                page.Annotations.Add(annotation);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Reconstructed PDF saved to '{outputPdfPath}'.");
    }
}