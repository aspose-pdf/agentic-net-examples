using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputHtmlPath = "output.html";       // generated HTML file
        const string outputFolder   = "HtmlOutput";       // folder for CSS and font files
        const string customFontFile = "customfont.woff";   // custom font file placed in outputFolder

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists (will hold CSS and font files)
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure HTML save options
            Aspose.Pdf.HtmlSaveOptions htmlOptions = new Aspose.Pdf.HtmlSaveOptions();

            // Save fonts as WOFF and keep them external (so we can reference them via @font-face)
            htmlOptions.FontSavingMode = Aspose.Pdf.HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF;

            // Custom CSS saving strategy – write a CSS file that defines @font-face
            htmlOptions.CustomCssSavingStrategy = (Aspose.Pdf.HtmlSaveOptions.CssSavingInfo cssInfo) =>
            {
                // Build CSS content with @font-face pointing to the custom font file
                string cssContent =
                    "@font-face {\n" +
                    "    font-family: 'MyCustomFont';\n" +
                    $"    src: url('{customFontFile}') format('woff');\n" +
                    "    font-weight: normal;\n" +
                    "    font-style: normal;\n" +
                    "}\n" +
                    "body {\n" +
                    "    font-family: 'MyCustomFont', sans-serif;\n" +
                    "}\n";

                // Determine the full path where the CSS should be written.
                // cssInfo.SupposedURL contains the file name the converter expects.
                string cssFilePath = Path.Combine(outputFolder, cssInfo.SupposedURL);

                // Write the CSS file.
                File.WriteAllText(cssFilePath, cssContent);
            };

            // Set the folder where the converter will place generated CSS files.
            // By default, CSS files are saved next to the HTML file; we redirect them to our folder.
            htmlOptions.SpecialFolderForAllImages = outputFolder; // reuse for CSS location

            // Save the PDF as HTML using the configured options.
            // The HTML file will be created in the same folder as the CSS files.
            pdfDoc.Save(Path.Combine(outputFolder, outputHtmlPath), htmlOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML with embedded custom font. Files are in '{outputFolder}'.");
    }
}