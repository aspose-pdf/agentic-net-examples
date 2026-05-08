using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

// Minimal NUnit stubs – added to satisfy compilation when the NUnit package is not referenced.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeSetUpAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // source PDF with a form
        const string outputPath = "output.pdf"; // destination PDF
        const string fieldName = "TextField1"; // name of the field to duplicate
        const int copies = 5;                    // how many copies to create
        const int pageNumber = 1;                // 1‑based page index where copies will be placed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the original field; the Form indexer returns a WidgetAnnotation, so cast to Field
            Field? originalField = doc.Form[fieldName] as Field;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
                return;
            }

            // Store the original rectangle to base new positions on it
            Aspose.Pdf.Rectangle baseRect = originalField.Rect;

            // Create the requested number of copies
            for (int i = 1; i <= copies; i++)
            {
                // New partial name for the duplicated field
                string newPartialName = $"{fieldName}_Copy{i}";

                // Add creates a copy of the field and places it on the given page; it returns a WidgetAnnotation, so cast to Field
                Field? newField = doc.Form.Add(originalField, newPartialName, pageNumber) as Field;
                if (newField == null)
                {
                    Console.Error.WriteLine($"Failed to duplicate field '{fieldName}' on iteration {i}.");
                    continue;
                }

                // Optionally shift each copy vertically (30 points per copy)
                float offsetY = i * 30f;
                Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(
                    baseRect.LLX,
                    baseRect.LLY - offsetY,
                    baseRect.URX,
                    baseRect.URY - offsetY);

                // Apply the new rectangle to the duplicated field
                newField.Rect = newRect;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicated fields saved to '{outputPath}'.");
    }
}
