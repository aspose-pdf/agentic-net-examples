// Program.cs
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_js.pdf";
        const string javaScript = "app.alert('Hello from JavaScript!');";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDoc = new Document(inputPath);

        // Add a document‑level JavaScript action (optional – shows alert when the PDF is opened).
        pdfDoc.OpenAction = new JavascriptAction(javaScript);

        // Add a page annotation that triggers the same JavaScript when the annotation is activated.
        // Here we use a LinkAnnotation with a rectangular area on the first page.
        Page firstPage = pdfDoc.Pages[1];
        Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
        LinkAnnotation jsAnnotation = new LinkAnnotation(firstPage, annotRect)
        {
            Action = new JavascriptAction(javaScript)
        };
        firstPage.Annotations.Add(jsAnnotation);

        // Save the modified PDF.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"JavaScript annotation added. Saved to '{outputPath}'.");
    }
}

// AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig (dummy source file to satisfy the project reference)
// This file is intentionally left empty. It exists only to prevent the CS2001 build error
// caused by a missing source file entry in the project file.

namespace Dummy
{
    // No implementation needed.
}
