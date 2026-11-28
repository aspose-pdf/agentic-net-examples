using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "schedule_form.pdf";
        const string outputPath = "daytype.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ---------------------------------------------------------------------
        // 1. Add the list items to the existing ListBox field "DayType" using
        //    FormEditor.AddListItem (the correct API for ListBox/ComboBox items).
        // ---------------------------------------------------------------------
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Populate the ListBox with the required options.
            editor.AddListItem("DayType", "Weekday");
            editor.AddListItem("DayType", "Weekend");

            // Save the intermediate PDF that now contains the items.
            editor.Save(outputPath);
        }

        // ---------------------------------------------------------------------
        // 2. Set the default selected value.  FormEditor does not modify field
        //    values; the Form facade is used for filling data.
        // ---------------------------------------------------------------------
        using (Form form = new Form())
        {
            form.BindPdf(outputPath);
            // Fill the ListBox with the default value (first item).
            form.FillField("DayType", "Weekday");
            form.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
