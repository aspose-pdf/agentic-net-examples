using System;
using System.IO;
using Aspose.Pdf;

namespace SetPageBackgroundExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to a simple configuration file that contains the theme name (e.g., Light, Dark, Sepia)
            string configPath = "theme.config";
            string theme = "Light"; // default theme

            if (File.Exists(configPath))
            {
                theme = File.ReadAllText(configPath).Trim();
            }

            // Create a new PDF document (self‑contained example)
            using (Document doc = new Document())
            {
                // Add a blank page to the document
                doc.Pages.Add();

                // Choose a background color based on the selected theme
                Aspose.Pdf.Color backgroundColor;
                if (string.Equals(theme, "Dark", StringComparison.OrdinalIgnoreCase))
                {
                    backgroundColor = Aspose.Pdf.Color.Black;
                }
                else if (string.Equals(theme, "Sepia", StringComparison.OrdinalIgnoreCase))
                {
                    backgroundColor = Aspose.Pdf.Color.SaddleBrown;
                }
                else // Light or any unrecognized value
                {
                    backgroundColor = Aspose.Pdf.Color.White;
                }

                // Apply the chosen background color to every page in the document
                foreach (Aspose.Pdf.Page page in doc.Pages)
                {
                    page.Background = backgroundColor;
                }

                // Save the modified PDF
                doc.Save("output.pdf");
            }
        }
    }
}
