using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "application_form_v5.pdf";
        const string outputPath = "roles.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor does not implement IDisposable, so we instantiate it directly.
        var editor = new FormEditor();

        // Bind the existing PDF document.
        editor.BindPdf(inputPath);

        // Add items to the existing ListBox field named "Roles".
        // The correct method on FormEditor is AddListItem (not AddListBoxItem).
        editor.AddListItem("Roles", "Admin");
        editor.AddListItem("Roles", "User");
        editor.AddListItem("Roles", "Guest");

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Roles list added and saved to '{outputPath}'.");
    }
}
