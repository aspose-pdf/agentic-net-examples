using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text.Json;

// Simple replacement for ASP.NET WebForms HiddenField control
public class HiddenField
{
    public string Value { get; set; } = string.Empty;
}

// The original class was a WebForms Page (System.Web.UI.Page) which is not available in
// .NET 5/6+ projects.  We replace the inheritance with a plain class that can be used in
// any .NET application (Console, ASP.NET Core, etc.).  The public API (LoadTaxRates) stays
// the same, so existing callers only need to instantiate TaxPage and call LoadTaxRates.
public class TaxPage
{
    // Hidden field that will hold the serialized tax rates for client‑side calculations
    protected HiddenField hiddenTaxRates = new HiddenField();

    // Simulates the original Page_Load behaviour.  In a real ASP.NET Core Razor page you
    // would call this from OnGet/OnPost as appropriate.
    public void Initialize(bool isPostBack, string xmlPath = @"C:\Data\taxrates.xml")
    {
        if (!isPostBack)
        {
            LoadTaxRates(xmlPath);
        }
    }

    private void LoadTaxRates(string xmlPath)
    {
        XDocument doc;
        try
        {
            // Load the XML document; XDocument implements IDisposable internally,
            // but Load does not require an explicit using block.
            doc = XDocument.Load(xmlPath);
        }
        catch (Exception)
        {
            // If loading fails, ensure the hidden field is empty and exit.
            hiddenTaxRates.Value = string.Empty;
            return;
        }

        // Expected XML format:
        // <TaxRates>
        //   <Rate country="US" value="0.07" />
        //   <Rate country="CA" value="0.13" />
        //   ...
        // </TaxRates>

        var rates = new Dictionary<string, decimal>();

        foreach (XElement rateElem in doc.Root.Elements("Rate"))
        {
            string country = (string)rateElem.Attribute("country");
            string valueStr = (string)rateElem.Attribute("value");

            if (string.IsNullOrWhiteSpace(country))
                continue;

            if (decimal.TryParse(valueStr, out decimal value))
                rates[country] = value;
        }

        // Serialize the dictionary to JSON so JavaScript can read it easily.
        // System.Text.Json is part of the base class library and does not require
        // System.Web extensions.
        string json = JsonSerializer.Serialize(rates);

        // Populate the hidden field with the JSON string.
        hiddenTaxRates.Value = json;
    }

    // Expose the hidden field value for callers (e.g., Razor view, API response)
    public string GetSerializedTaxRates() => hiddenTaxRates.Value;
}

// Entry‑point required for a console‑style project.
public class Program
{
    public static void Main(string[] args)
    {
        // Allow the XML path to be supplied via command line; fall back to a default.
        string xmlPath = args.Length > 0 ? args[0] : "taxrates.xml";

        var taxPage = new TaxPage();
        taxPage.Initialize(isPostBack: false, xmlPath: xmlPath);

        Console.WriteLine(taxPage.GetSerializedTaxRates());
    }
}