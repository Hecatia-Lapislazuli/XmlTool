using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using CsvHelper;
using static XmlTool.Program;

namespace XmlTool {
    public class ClanConverter {
		public static void Clans_CSVtoXML(string fileInput, string fileOutput, XmlWriter localizationWriter, XmlWriter module_strings_writer) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			ClanRecord kingdomsRecord = new ClanRecord();
			IEnumerable<ClanRecord> records = csv.EnumerateRecords(kingdomsRecord);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("Factions");

				writeHeadderComment(writer);

				foreach (ClanRecord record in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("Faction");

					//Changes
					record.is_bandit = record.is_bandit.ToLower();
					record.is_clan_type_mercenary = record.is_clan_type_mercenary.ToLower();
					record.is_mafia = record.is_mafia.ToLower();
					record.is_minor_faction = record.is_minor_faction.ToLower();
					record.is_nomad = record.is_nomad.ToLower();
					record.is_outlaw = record.is_outlaw.ToLower();
					record.is_sect = record.is_sect.ToLower();

					//Defaults
					if (record.tier.Equals("")) record.tier = "4";

					//Temporary

					//Write
					writer.WriteAttributeString("id", record.id);
					writer.WriteAttributeString("tier", record.tier);
					if (!record.owner.Equals("")) writer.WriteAttributeString("owner", "Hero." + record.owner);
					if (!record.label_color.Equals("")) writer.WriteAttributeString("label_color", record.label_color);
					if (!record.color.Equals("")) writer.WriteAttributeString("color", record.color);
					if (!record.color2.Equals("")) writer.WriteAttributeString("color2", record.color2);
					if (!record.alternative_color.Equals("")) writer.WriteAttributeString("alternative_color", record.alternative_color);
					if (!record.alternative_color2.Equals("")) writer.WriteAttributeString("alternative_color2", record.alternative_color2);
					if (!record.culture.Equals("")) writer.WriteAttributeString("culture", "Culture." + record.culture);
					if (!record.super_faction.Equals("")) writer.WriteAttributeString("super_faction", "Kingdom." + record.super_faction);
					if (!record.default_party_template.Equals("")) writer.WriteAttributeString("default_party_template", record.default_party_template);
					if (!record.is_bandit.Equals("")) writer.WriteAttributeString("is_bandit", record.is_bandit);
					if (!record.is_minor_faction.Equals("")) writer.WriteAttributeString("is_minor_faction", record.is_minor_faction);
					if (!record.is_outlaw.Equals("")) writer.WriteAttributeString("is_outlaw", record.is_outlaw);
					if (!record.is_clan_type_mercenary.Equals("")) writer.WriteAttributeString("is_clan_type_mercenary", record.is_clan_type_mercenary);
					if (!record.is_nomad.Equals("")) writer.WriteAttributeString("is_nomad", record.is_nomad);
					if (!record.is_sect.Equals("")) writer.WriteAttributeString("is_sect", record.is_sect);
					if (!record.is_mafia.Equals("")) writer.WriteAttributeString("is_mafia", record.is_mafia);
					if (!record.settlement_banner_mesh.Equals("")) writer.WriteAttributeString("settlement_banner_mesh", record.settlement_banner_mesh);
					if (!record.flag_mesh.Equals("")) writer.WriteAttributeString("flag_mesh", record.flag_mesh);
					writer.WriteAttributeString("name", "{=Factions.Faction." + record.id + ".name}" + record.name);

					writeLocalizationNode(localizationWriter, "Factions.Faction." + record.id + ".name", record.name);

					if (!record.short_name.Equals("")) writer.WriteAttributeString("short_name", "{=Factions.Faction." + record.id + ".short_name}" + record.short_name);

					if (!record.short_name.Equals("")) writeLocalizationNode(localizationWriter, "Factions.Faction." + record.id + ".short_name", record.short_name);

					if (!record.text.Equals("")) writer.WriteAttributeString("text", "{=Factions.Faction." + record.id + ".text}" + record.text);

					if (!record.text.Equals("")) writeLocalizationNode(localizationWriter, "Factions.Faction." + record.id + ".text", record.text);


					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		public class ClanRecord {
			public string id { get; set; }
			public string tier { get; set; }
			public string owner { get; set; }
			public string label_color { get; set; }
			public string color { get; set; }
			public string color2 { get; set; }
			public string alternative_color { get; set; }
			public string alternative_color2 { get; set; }
			public string culture { get; set; }
			public string super_faction { get; set; }
			public string default_party_template { get; set; }
			public string is_bandit { get; set; }
			public string is_minor_faction { get; set; }
			public string is_outlaw { get; set; }
			public string is_clan_type_mercenary { get; set; }
			public string is_nomad { get; set; }
			public string is_sect { get; set; }
			public string is_mafia { get; set; }
			public string settlement_banner_mesh { get; set; }
			public string flag_mesh { get; set; }
			public string name { get; set; }
			public string short_name { get; set; }
			public string text { get; set; }
		}
	}
}
