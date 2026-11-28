using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "output113.pdf";
        const string fieldName = "DiscountCode";
        const string copiedFieldName = "DiscountCodeCopy"; // new name to avoid conflict
        const int targetPage = 2;
        const float posX = 210f; // lower‑left X coordinate
        const float posY = 210f; // lower‑left Y coordinate
        // Width/height – using a reasonable default (e.g., 100x20). Adjust as needed.
        const float width = 100f;
        const float height = 20f;

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // FormEditor implements IDisposable – wrap it in a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the source PDF document.
            editor.BindPdf(sourcePath);

            // Duplicate the field definition on the target page.
            // CopyInnerField copies the field within the same document.
            editor.CopyInnerField(fieldName, copiedFieldName, targetPage);

            // Re‑position the copied field to the desired coordinates.
            // MoveField expects lower‑left (llx,lly) and upper‑right (urx,ury) coordinates.
            editor.MoveField(copiedFieldName, posX, posY, posX + width, posY + height);

            // Set the background colour of the copied field to pink via the Facade.
            editor.Facade = new FormFieldFacade
            {
                BackgroundColor = Color.Pink
            };
            editor.DecorateField(copiedFieldName);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' copied to page {targetPage} as '{copiedFieldName}' with pink background and saved as '{outputPath}'.");
    }
}
