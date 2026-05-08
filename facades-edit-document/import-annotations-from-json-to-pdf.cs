using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Drawing;               // System.Drawing.Rectangle is required by PdfContentEditor
using Aspose.Pdf.Facades;          // PdfContentEditor resides here

// Model that matches the JSON structure for an annotation definition
public class AnnotationDefinition
{
    public string Title { get; set; }      // Annotation title
    public string Contents { get; set; }   // Annotation contents
    public int Page { get; set; }          // 1‑based page number where the annotation will be placed
    public int X { get; set; }             // Left coordinate (points)
    public int Y { get; set; }             // Bottom coordinate (points)
    public int Width { get; set; }         // Width of the annotation rectangle
    public int Height { get; set; }        // Height of the annotation rectangle
    public bool Open { get; set; }         // Whether the annotation is initially open
    public string Icon { get; set; }       // Icon name (e.g., "Comment", "Key", "Note", etc.)
}

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";        // Source PDF
        const string jsonPath       = "annotations.json"; // JSON file with annotation definitions
        const string outputPdfPath  = "output.pdf";       // Result PDF

        // Validate input files
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

        // Deserialize JSON into a list of annotation definitions
        List<AnnotationDefinition> annotations;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            annotations = JsonSerializer.Deserialize<List<AnnotationDefinition>>(jsonContent);
            if (annotations == null)
                throw new InvalidOperationException("Deserialized annotation list is null.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read or parse JSON: {ex.Message}");
            return;
        }

        // Apply annotations using PdfContentEditor
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPdfPath);

            // Create each annotation as defined in the JSON
            foreach (var ann in annotations)
            {
                // Build a System.Drawing.Rectangle for the annotation bounds
                Rectangle rect = new Rectangle(ann.X, ann.Y, ann.Width, ann.Height);

                // Create the text annotation
                // Parameters: rectangle, title, contents, open flag, icon name, page number
                editor.CreateText(rect, ann.Title, ann.Contents, ann.Open, ann.Icon, ann.Page);
            }

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations imported and PDF saved to '{outputPdfPath}'.");
    }
}