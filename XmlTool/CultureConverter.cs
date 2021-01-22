using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using CsvHelper;
using static XmlTool.Program;

namespace XmlTool {
    public class CultureConverter {
		public static void Cultures_CSVtoXML(string fileInput, string fileOutput, XmlWriter localizationWriter, XmlWriter module_strings_writer) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			CultureRecord cultureRecord = new CultureRecord();
			IEnumerable<CultureRecord> records = csv.EnumerateRecords(cultureRecord);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("SPCultures");

				writeHeadderComment(writer);

				foreach (CultureRecord record in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("VANILLA")) break;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("Culture");

					//Changes
					record.is_main_culture = record.is_main_culture.ToLower();
					record.is_bandit = record.is_bandit.ToLower();
					record.can_have_settlement = record.can_have_settlement.ToLower();
					//						
					//Defaults
					if (record.default_party_template.Equals("")) record.default_party_template = "kingdom_hero_party_empire_template";
					if (record.villager_party_template.Equals("")) record.villager_party_template = "villager_empire_template";
					if (record.elite_caravan_party_template.Equals("")) record.elite_caravan_party_template = "elite_caravan_template_empire";
					if (record.bandit_boss_party_template.Equals("") && record.is_bandit.Equals("true")) record.bandit_boss_party_template = "kingdom_hero_party_empire_template";
					if (record.caravan_party_template.Equals("")) record.caravan_party_template = "caravan_template_empire";
					if (record.militia_party_template.Equals("")) record.militia_party_template = "militia_empire_template";
					if (record.rebels_party_template.Equals("")) record.rebels_party_template = "rebels_empire_template";

					if (record.melee_militia_troop.Equals("")) record.melee_militia_troop = record.basic_troop;
					if (record.melee_elite_militia_troop.Equals("")) record.melee_elite_militia_troop = record.elite_basic_troop;
					if (record.ranged_militia_troop.Equals("")) record.ranged_militia_troop = record.basic_troop;
					if (record.ranged_elite_militia_troop.Equals("")) record.ranged_elite_militia_troop = record.elite_basic_troop;

					if (record.tournament_master.Equals("")) record.tournament_master = record.elite_basic_troop;
					if (record.caravan_master.Equals("")) record.caravan_master = record.elite_basic_troop;
					if (record.armed_trader.Equals("")) record.armed_trader = record.elite_basic_troop;
					if (record.caravan_guard.Equals("")) record.caravan_guard = record.elite_basic_troop;
					if (record.veteran_caravan_guard.Equals("")) record.veteran_caravan_guard = record.elite_basic_troop;
					if (record.duel_preset.Equals("")) record.duel_preset = record.elite_basic_troop;
					if (record.prison_guard.Equals("")) record.prison_guard = record.elite_basic_troop;
					if (record.guard.Equals("")) record.guard = record.elite_basic_troop;
					if (record.steward.Equals("")) record.steward = record.elite_basic_troop;
					if (record.blacksmith.Equals("")) record.blacksmith = record.elite_basic_troop;
					if (record.weaponsmith.Equals("")) record.weaponsmith = record.elite_basic_troop;

					if (record.townswoman.Equals("")) record.townswoman = record.elite_basic_troop;
					if (record.townswoman_infant.Equals("")) record.townswoman_infant = record.elite_basic_troop;
					if (record.townswoman_child.Equals("")) record.townswoman_child = record.elite_basic_troop;
					if (record.townswoman_teenager.Equals("")) record.townswoman_teenager = record.elite_basic_troop;
					if (record.townsman.Equals("")) record.townsman = record.elite_basic_troop;
					if (record.townsman_infant.Equals("")) record.townsman_infant = record.elite_basic_troop;
					if (record.townsman_child.Equals("")) record.townsman_child = record.elite_basic_troop;
					if (record.townsman_teenager.Equals("")) record.townsman_teenager = record.elite_basic_troop;

					if (record.villager.Equals("")) record.villager = record.elite_basic_troop;
					if (record.village_woman.Equals("")) record.village_woman = record.elite_basic_troop;
					if (record.villager_male_child.Equals("")) record.villager_male_child = record.elite_basic_troop;
					if (record.villager_male_teenager.Equals("")) record.villager_male_teenager = record.elite_basic_troop;
					if (record.villager_female_child.Equals("")) record.villager_female_child = record.elite_basic_troop;
					if (record.villager_female_teenager.Equals("")) record.villager_female_teenager = record.elite_basic_troop;

					if (record.ransom_broker.Equals("")) record.ransom_broker = record.elite_basic_troop;
					if (record.gangleader_bodyguard.Equals("")) record.gangleader_bodyguard = record.elite_basic_troop;
					if (record.merchant_notary.Equals("")) record.merchant_notary = record.elite_basic_troop;
					if (record.preacher_notary.Equals("")) record.preacher_notary = record.elite_basic_troop;
					if (record.rural_notable_notary.Equals("")) record.rural_notable_notary = record.elite_basic_troop;
					if (record.shop_worker.Equals("")) record.shop_worker = record.elite_basic_troop;

					if (record.tavernkeeper.Equals("")) record.tavernkeeper = record.elite_basic_troop;
					if (record.taverngamehost.Equals("")) record.taverngamehost = record.elite_basic_troop;
					if (record.musician.Equals("")) record.musician = record.elite_basic_troop;
					if (record.tavern_wench.Equals("")) record.tavern_wench = record.elite_basic_troop;

					if (record.armorer.Equals("")) record.armorer = record.elite_basic_troop;
					if (record.horseMerchant.Equals("")) record.horseMerchant = record.elite_basic_troop;
					if (record.barber.Equals("")) record.barber = record.elite_basic_troop;
					if (record.merchant.Equals("")) record.merchant = record.elite_basic_troop;
					if (record.beggar.Equals("")) record.beggar = record.elite_basic_troop;
					if (record.female_beggar.Equals("")) record.female_beggar = record.elite_basic_troop;
					if (record.female_dancer.Equals("")) record.female_dancer = record.elite_basic_troop;

					if (record.gear_practice_dummy.Equals("")) record.gear_practice_dummy = record.elite_basic_troop;
					if (record.weapon_practice_stage_1.Equals("")) record.weapon_practice_stage_1 = record.elite_basic_troop;
					if (record.weapon_practice_stage_2.Equals("")) record.weapon_practice_stage_2 = record.elite_basic_troop;
					if (record.weapon_practice_stage_3.Equals("")) record.weapon_practice_stage_3 = record.elite_basic_troop;

					if (record.gear_dummy.Equals("")) record.gear_dummy = record.elite_basic_troop;

					if (record.is_bandit.Equals("true")) {
						if (record.bandit_bandit.Equals("")) record.bandit_bandit = record.elite_basic_troop;
						if (record.bandit_chief.Equals("")) record.bandit_chief = record.elite_basic_troop;
						if (record.bandit_raider.Equals("")) record.bandit_raider = record.elite_basic_troop;
						if (record.bandit_boss.Equals("")) record.bandit_boss = record.elite_basic_troop;
					}

					if (record.board_game_type.Equals("")) record.board_game_type = "Tablut";

					/*
					if (record.tournament_template_one_participant_set_v1.Equals("")) record.tournament_template_one_participant_set_v1 = record.elite_basic_troop;
					if (record.tournament_template_one_participant_set_v2.Equals("")) record.tournament_template_one_participant_set_v2 = record.elite_basic_troop;
					if (record.tournament_template_one_participant_set_v3.Equals("")) record.tournament_template_one_participant_set_v3 = record.elite_basic_troop;
					if (record.tournament_template_one_participant_set_v4.Equals("")) record.tournament_template_one_participant_set_v4 = record.elite_basic_troop;

					if (record.tournament_template_two_participant_set_v1.Equals("")) record.tournament_template_two_participant_set_v1 = record.elite_basic_troop;
					if (record.tournament_template_two_participant_set_v2.Equals("")) record.tournament_template_two_participant_set_v2 = record.elite_basic_troop;
					if (record.tournament_template_two_participant_set_v3.Equals("")) record.tournament_template_two_participant_set_v3 = record.elite_basic_troop;
					if (record.tournament_template_two_participant_set_v4.Equals("")) record.tournament_template_two_participant_set_v4 = record.elite_basic_troop;

					if (record.tournament_template_four_participant_set_v1.Equals("")) record.tournament_template_four_participant_set_v1 = record.elite_basic_troop;
					if (record.tournament_template_four_participant_set_v2.Equals("")) record.tournament_template_four_participant_set_v2 = record.elite_basic_troop;
					if (record.tournament_template_four_participant_set_v3.Equals("")) record.tournament_template_four_participant_set_v3 = record.elite_basic_troop;
					if (record.tournament_template_four_participant_set_v4.Equals("")) record.tournament_template_four_participant_set_v4 = record.elite_basic_troop;
					*/

					//Temporary


					//Write
					writer.WriteAttributeString("id", record.id);
					writer.WriteAttributeString("name", "{=Cultures.Culture." + record.id + ".name}" + record.name);

					writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".name", record.name);

					if (!record.is_main_culture.Equals("")) writer.WriteAttributeString("is_main_culture", record.is_main_culture);
					if (!record.can_have_settlement.Equals("")) writer.WriteAttributeString("can_have_settlement", record.can_have_settlement);
					if (!record.town_edge_number.Equals("")) writer.WriteAttributeString("town_edge_number", record.town_edge_number);
					if (!record.militia_bonus.Equals("")) writer.WriteAttributeString("militia_bonus", record.militia_bonus);
					if (!record.prosperity_bonus.Equals("")) writer.WriteAttributeString("prosperity_bonus", record.prosperity_bonus);
					if (!record.encounter_background_mesh.Equals("")) writer.WriteAttributeString("encounter_background_mesh", record.encounter_background_mesh);
					writer.WriteAttributeString("basic_troop", "NPCCharacter." + record.basic_troop);
					writer.WriteAttributeString("elite_basic_troop", "NPCCharacter." + record.elite_basic_troop);

					if (!record.is_bandit.Equals("")) writer.WriteAttributeString("is_bandit", record.is_bandit);
					if (!record.default_face_key.Equals("")) writer.WriteAttributeString("default_face_key", record.default_face_key);

					if (!record.default_party_template.Equals("")) writer.WriteAttributeString("default_party_template", "PartyTemplate." + record.default_party_template);
					if (!record.villager_party_template.Equals("")) writer.WriteAttributeString("villager_party_template", "PartyTemplate." + record.villager_party_template);
					if (!record.elite_caravan_party_template.Equals("")) writer.WriteAttributeString("elite_caravan_party_template", "PartyTemplate." + record.elite_caravan_party_template);
					if (!record.bandit_boss_party_template.Equals("")) writer.WriteAttributeString("bandit_boss_party_template", "PartyTemplate." + record.bandit_boss_party_template);
					if (!record.caravan_party_template.Equals("")) writer.WriteAttributeString("caravan_party_template", "PartyTemplate." + record.caravan_party_template);
					if (!record.militia_party_template.Equals("")) writer.WriteAttributeString("militia_party_template", "PartyTemplate." + record.militia_party_template);
					if (!record.rebels_party_template.Equals("")) writer.WriteAttributeString("rebels_party_template", "PartyTemplate." + record.rebels_party_template);

					if (!record.melee_militia_troop.Equals("")) writer.WriteAttributeString("melee_militia_troop", "NPCCharacter." + record.melee_militia_troop);
					if (!record.melee_elite_militia_troop.Equals("")) writer.WriteAttributeString("melee_elite_militia_troop", "NPCCharacter." + record.melee_elite_militia_troop);
					if (!record.ranged_militia_troop.Equals("")) writer.WriteAttributeString("ranged_militia_troop", "NPCCharacter." + record.ranged_militia_troop);
					if (!record.ranged_elite_militia_troop.Equals("")) writer.WriteAttributeString("ranged_elite_militia_troop", "NPCCharacter." + record.ranged_elite_militia_troop);

					if (!record.tournament_master.Equals("")) writer.WriteAttributeString("tournament_master", "NPCCharacter." + record.tournament_master);
					if (!record.caravan_master.Equals("")) writer.WriteAttributeString("caravan_master", "NPCCharacter." + record.caravan_master);
					if (!record.armed_trader.Equals("")) writer.WriteAttributeString("armed_trader", "NPCCharacter." + record.armed_trader);
					if (!record.caravan_guard.Equals("")) writer.WriteAttributeString("caravan_guard", "NPCCharacter." + record.caravan_guard);
					if (!record.veteran_caravan_guard.Equals("")) writer.WriteAttributeString("veteran_caravan_guard", "NPCCharacter." + record.veteran_caravan_guard);

					if (!record.duel_preset.Equals("")) writer.WriteAttributeString("duel_preset", "NPCCharacter." + record.duel_preset);

					if (!record.prison_guard.Equals("")) writer.WriteAttributeString("prison_guard", "NPCCharacter." + record.prison_guard);
					if (!record.guard.Equals("")) writer.WriteAttributeString("guard", "NPCCharacter." + record.guard);
					if (!record.steward.Equals("")) writer.WriteAttributeString("steward", "NPCCharacter." + record.steward);
					if (!record.blacksmith.Equals("")) writer.WriteAttributeString("blacksmith", "NPCCharacter." + record.blacksmith);
					if (!record.weaponsmith.Equals("")) writer.WriteAttributeString("weaponsmith", "NPCCharacter." + record.weaponsmith);

					if (!record.townswoman.Equals("")) writer.WriteAttributeString("townswoman", "NPCCharacter." + record.townswoman);
					if (!record.townswoman_infant.Equals("")) writer.WriteAttributeString("townswoman_infant", "NPCCharacter." + record.townswoman_infant);
					if (!record.townswoman_child.Equals("")) writer.WriteAttributeString("townswoman_child", "NPCCharacter." + record.townswoman_child);
					if (!record.townswoman_teenager.Equals("")) writer.WriteAttributeString("townswoman_teenager", "NPCCharacter." + record.townswoman_teenager);
					if (!record.townsman.Equals("")) writer.WriteAttributeString("townsman", "NPCCharacter." + record.townsman);
					if (!record.townsman_infant.Equals("")) writer.WriteAttributeString("townsman_infant", "NPCCharacter." + record.townsman_infant);
					if (!record.townsman_child.Equals("")) writer.WriteAttributeString("townsman_child", "NPCCharacter." + record.townsman_child);
					if (!record.townsman_teenager.Equals("")) writer.WriteAttributeString("townsman_teenager", "NPCCharacter." + record.townsman_teenager);

					if (!record.villager.Equals("")) writer.WriteAttributeString("villager", "NPCCharacter." + record.villager);
					if (!record.village_woman.Equals("")) writer.WriteAttributeString("villager_woman", "NPCCharacter." + record.village_woman);
					if (!record.villager_male_child.Equals("")) writer.WriteAttributeString("villager_male_child", "NPCCharacter." + record.villager_male_child);
					if (!record.villager_male_teenager.Equals("")) writer.WriteAttributeString("villager_male_teenager", "NPCCharacter." + record.villager_male_teenager);
					if (!record.villager_female_child.Equals("")) writer.WriteAttributeString("villager_female_child", "NPCCharacter." + record.villager_female_child);
					if (!record.villager_female_teenager.Equals("")) writer.WriteAttributeString("villager_female_teenager", "NPCCharacter." + record.villager_female_teenager);

					if (!record.ransom_broker.Equals("")) writer.WriteAttributeString("ransom_broker", "NPCCharacter." + record.ransom_broker);

					if (!record.gangleader_bodyguard.Equals("")) writer.WriteAttributeString("gangleader_bodyguard", "NPCCharacter." + record.gangleader_bodyguard);

					if (!record.merchant_notary.Equals("")) writer.WriteAttributeString("merchant_notary", "NPCCharacter." + record.merchant_notary);
					if (!record.preacher_notary.Equals("")) writer.WriteAttributeString("preacher_notary", "NPCCharacter." + record.preacher_notary);
					if (!record.rural_notable_notary.Equals("")) writer.WriteAttributeString("rural_notable_notary", "NPCCharacter." + record.rural_notable_notary);

					if (!record.shop_worker.Equals("")) writer.WriteAttributeString("shop_worker", "NPCCharacter." + record.shop_worker);

					if (!record.tavernkeeper.Equals("")) writer.WriteAttributeString("tavernkeeper", "NPCCharacter." + record.tavernkeeper);
					if (!record.taverngamehost.Equals("")) writer.WriteAttributeString("taverngamehost", "NPCCharacter." + record.taverngamehost);
					if (!record.musician.Equals("")) writer.WriteAttributeString("musician", "NPCCharacter." + record.musician);
					if (!record.tavern_wench.Equals("")) writer.WriteAttributeString("tavern_wench", "NPCCharacter." + record.tavern_wench);

					if (!record.armorer.Equals("")) writer.WriteAttributeString("armorer", "NPCCharacter." + record.armorer);
					if (!record.horseMerchant.Equals("")) writer.WriteAttributeString("horseMerchant", "NPCCharacter." + record.horseMerchant);
					if (!record.barber.Equals("")) writer.WriteAttributeString("barber", "NPCCharacter." + record.barber);
					if (!record.merchant.Equals("")) writer.WriteAttributeString("merchant", "NPCCharacter." + record.merchant);

					if (!record.beggar.Equals("")) writer.WriteAttributeString("beggar", "NPCCharacter." + record.beggar);
					if (!record.female_beggar.Equals("")) writer.WriteAttributeString("female_beggar", "NPCCharacter." + record.female_beggar);

					if (!record.female_dancer.Equals("")) writer.WriteAttributeString("female_dancer", "NPCCharacter." + record.female_dancer);

					if (!record.gear_practice_dummy.Equals("")) writer.WriteAttributeString("gear_practice_dummy", "NPCCharacter." + record.gear_practice_dummy);
					if (!record.weapon_practice_stage_1.Equals("")) writer.WriteAttributeString("weapon_practice_stage_1", "NPCCharacter." + record.weapon_practice_stage_1);
					if (!record.weapon_practice_stage_2.Equals("")) writer.WriteAttributeString("weapon_practice_stage_2", "NPCCharacter." + record.weapon_practice_stage_2);
					if (!record.weapon_practice_stage_3.Equals("")) writer.WriteAttributeString("weapon_practice_stage_3", "NPCCharacter." + record.weapon_practice_stage_3);
					if (!record.gear_dummy.Equals("")) writer.WriteAttributeString("gear_dummy", "NPCCharacter." + record.gear_dummy);

					if (!record.bandit_bandit.Equals("")) writer.WriteAttributeString("bandit_bandit", "NPCCharacter." + record.bandit_bandit);
					if (!record.bandit_chief.Equals("")) writer.WriteAttributeString("bandit_chief", "NPCCharacter." + record.bandit_chief);
					if (!record.bandit_raider.Equals("")) writer.WriteAttributeString("bandit_raider", "NPCCharacter." + record.bandit_raider);
					if (!record.bandit_boss.Equals("")) writer.WriteAttributeString("bandit_boss", "NPCCharacter." + record.bandit_boss);

					if (!record.board_game_type.Equals("")) writer.WriteAttributeString("board_game_type", record.board_game_type);

					if (!record.male_names.Equals("")) {
						writer.WriteStartElement("male_names");

						string[] names = record.male_names.Split(";");

						foreach (string name in names) {
							writer.WriteStartElement("name");
							writer.WriteAttributeString("name", "{Cultures.Culture." + record.id + ".male_names." + name.ToLower() + "}" + name);
							writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".male_names." + name.ToLower(), name);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}
					if (!record.female_names.Equals("")) {
						writer.WriteStartElement("female_names");

						string[] names = record.female_names.Split(";");

						foreach (string name in names) {
							writer.WriteStartElement("name");
							writer.WriteAttributeString("name", "{Cultures.Culture." + record.id + ".female_names." + name.ToLower() + "}" + name);
							writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".female_names." + name.ToLower(), name);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}
					if (!record.clan_names.Equals("")) {
						writer.WriteStartElement("clan_names");

						string[] names = record.clan_names.Split(";");

						foreach (string name in names) {
							writer.WriteStartElement("name");
							writer.WriteAttributeString("name", "{Cultures.Culture." + record.id + ".clan_names." + name.ToLower() + "}" + name);
							writeLocalizationNode(localizationWriter, "Culture.Culture." + record.id + ".clan_names." + name.ToLower(), name);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					if (!record.tournament_template_two_participant_set_v1.Equals("") || !record.tournament_template_two_participant_set_v2.Equals("") || !record.tournament_template_two_participant_set_v3.Equals("") || !record.tournament_template_two_participant_set_v4.Equals("")) {
						writer.WriteStartElement("tournament_team_templates_two_participant");

						if (!record.tournament_template_two_participant_set_v1.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_two_participant_set_v1);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_two_participant_set_v2.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_two_participant_set_v2);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_two_participant_set_v3.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_two_participant_set_v3);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_two_participant_set_v4.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_two_participant_set_v4);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					if (!record.tournament_template_four_participant_set_v1.Equals("") || !record.tournament_template_four_participant_set_v2.Equals("") || !record.tournament_template_four_participant_set_v3.Equals("") || !record.tournament_template_four_participant_set_v4.Equals("")) {
						writer.WriteStartElement("tournament_team_templates_four_participant");

						if (!record.tournament_template_four_participant_set_v1.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_four_participant_set_v1);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_four_participant_set_v2.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_four_participant_set_v2);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_four_participant_set_v3.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_four_participant_set_v3);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_four_participant_set_v4.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_four_participant_set_v4);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					if (!record.tournament_template_one_participant_set_v1.Equals("") || !record.tournament_template_one_participant_set_v2.Equals("") || !record.tournament_template_one_participant_set_v3.Equals("") || !record.tournament_template_one_participant_set_v4.Equals("")) {
						writer.WriteStartElement("tournament_team_templates_one_participant");

						if (!record.tournament_template_one_participant_set_v1.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_one_participant_set_v1);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_one_participant_set_v2.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_one_participant_set_v2);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_one_participant_set_v3.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_one_participant_set_v3);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_one_participant_set_v4.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_one_participant_set_v4);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					if (!record.str_culture_description.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_culture_description", record.str_culture_description);
						writeLocalizationNode(module_strings_writer, "str_culture_description." + record.id, "{=Cultures.Culture." + record.id + ".str_culture_description}" + record.str_culture_description);
					}
					if (!record.str_culture_rich_name.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_culture_rich_name", record.str_culture_rich_name);
						writeLocalizationNode(module_strings_writer, "str_culture_rich_name." + record.id, "{=Cultures.Culture." + record.id + ".str_culture_rich_name}" + record.str_culture_rich_name);
					}
					if (!record.str_faction_official.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_official", record.str_faction_official);
						writeLocalizationNode(module_strings_writer, "str_faction_official." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_official}" + record.str_faction_official);
					}
					if (!record.str_faction_official_f.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_official_f", record.str_faction_official_f);
						writeLocalizationNode(module_strings_writer, "str_faction_official_f." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_official_f}" + record.str_faction_official_f);
					}
					if (!record.str_faction_ruler.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_ruler", record.str_faction_ruler);
						writeLocalizationNode(module_strings_writer, "str_faction_ruler." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_ruler}" + record.str_faction_ruler);
					}
					if (!record.str_faction_ruler_f.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_ruler_f", record.str_faction_ruler_f);
						writeLocalizationNode(module_strings_writer, "str_faction_ruler_f." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_ruler_f}" + record.str_faction_ruler_f);
					}
					if (!record.str_faction_ruler_term_in_speech.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_ruler_term_in_speech", record.str_faction_ruler_term_in_speech);
						writeLocalizationNode(module_strings_writer, "str_faction_ruler_term_in_speech." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_ruler_term_in_speech}" + record.str_faction_ruler_term_in_speech);
					}
					if (!record.str_faction_ruler_name_with_title.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_ruler_name_with_title", record.str_faction_ruler_name_with_title);
						writeLocalizationNode(module_strings_writer, "str_faction_ruler_name_with_title." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_ruler_name_with_title}" + record.str_faction_ruler_name_with_title);
					}
					if (!record.str_faction_noble_name_with_title.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_noble_name_with_title", record.str_faction_noble_name_with_title);
						writeLocalizationNode(module_strings_writer, "str_faction_noble_name_with_title." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_noble_name_with_title}" + record.str_faction_noble_name_with_title);
					}
					if (!record.str_adjective_for_faction.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_adjective_for_faction", record.str_adjective_for_faction);
						writeLocalizationNode(module_strings_writer, "str_adjective_for_faction." + record.id, "{=Cultures.Culture." + record.id + ".str_adjective_for_faction}" + record.str_adjective_for_faction);
					}
					if (!record.str_short_term_for_faction.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_short_term_for_faction", record.str_short_term_for_faction);
						writeLocalizationNode(module_strings_writer, "str_short_term_for_faction." + record.id, "{=Cultures.Culture." + record.id + ".str_short_term_for_faction}" + record.str_short_term_for_faction);
					}
					if (!record.str_faction_formal_name_for_culture.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_formal_name_for_culture", record.str_faction_formal_name_for_culture);
						writeLocalizationNode(module_strings_writer, "str_faction_formal_name_for_culture." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_formal_name_for_culture}" + record.str_faction_formal_name_for_culture);
					}
					if (!record.str_neutral_term_for_culture.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_neutral_term_for_culture", record.str_neutral_term_for_culture);
						writeLocalizationNode(module_strings_writer, "str_neutral_term_for_culture." + record.id, "{=Cultures.Culture." + record.id + ".str_neutral_term_for_culture}" + record.str_neutral_term_for_culture);
					}

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}

		public static void Cultures_XMLtoCSV(string xmlInput, string csvOutput) {
			List<CultureRecord> records = new List<CultureRecord>();

			using (XmlReader xmlReader = XmlReader.Create(xmlInput)) {
				while (xmlReader.Read()) {
					if (xmlReader.NodeType != XmlNodeType.Element) continue;
					//Console.WriteLine(xmlReader.Name);

					if (xmlReader.Name.Equals("Culture")) {
					NewPartyRecord:
						CultureRecord record = new CultureRecord();

						record.id = xmlReader.GetAttribute("id");
						record.name = Trim(xmlReader.GetAttribute("name"));
						record.is_main_culture = xmlReader.GetAttribute("is_main_culture");
						record.can_have_settlement = xmlReader.GetAttribute("can_have_settlement");
						record.town_edge_number = xmlReader.GetAttribute("town_edge_number");
						record.militia_bonus = xmlReader.GetAttribute("militia_bonus");
						record.prosperity_bonus = xmlReader.GetAttribute("prosperity_bonus");
						record.encounter_background_mesh = xmlReader.GetAttribute("encounter_background_mesh");

						record.basic_troop = Trim(xmlReader.GetAttribute("basic_troop"));
						record.elite_basic_troop = Trim(xmlReader.GetAttribute("elite_basic_troop"));

						record.is_bandit = xmlReader.GetAttribute("is_bandit");
						record.default_face_key = xmlReader.GetAttribute("default_face_key");

						record.default_party_template = Trim(xmlReader.GetAttribute("default_party_template"));
						record.villager_party_template = Trim(xmlReader.GetAttribute("villager_party_template"));
						record.elite_caravan_party_template = Trim(xmlReader.GetAttribute("elite_caravan_party_template"));
						record.bandit_boss_party_template = Trim(xmlReader.GetAttribute("bandit_boss_party_template"));
						record.caravan_party_template = Trim(xmlReader.GetAttribute("caravan_party_template"));
						record.militia_party_template = Trim(xmlReader.GetAttribute("militia_party_template"));
						record.rebels_party_template = Trim(xmlReader.GetAttribute("rebels_party_template"));

						record.melee_militia_troop = Trim(xmlReader.GetAttribute("melee_militia_troop"));
						record.melee_elite_militia_troop = Trim(xmlReader.GetAttribute("melee_elite_militia_troop"));
						record.ranged_militia_troop = Trim(xmlReader.GetAttribute("ranged_militia_troop"));
						record.ranged_elite_militia_troop = Trim(xmlReader.GetAttribute("ranged_elite_militia_troop"));
						record.tournament_master = Trim(xmlReader.GetAttribute("tournament_master"));
						record.caravan_master = Trim(xmlReader.GetAttribute("caravan_master"));
						record.armed_trader = Trim(xmlReader.GetAttribute("armed_trader"));
						record.caravan_guard = Trim(xmlReader.GetAttribute("caravan_guard"));
						record.veteran_caravan_guard = Trim(xmlReader.GetAttribute("veteran_caravan_guard"));

						record.duel_preset = Trim(xmlReader.GetAttribute("duel_preset"));
						record.prison_guard = Trim(xmlReader.GetAttribute("prison_guard"));
						record.guard = Trim(xmlReader.GetAttribute("guard"));
						record.steward = Trim(xmlReader.GetAttribute("steward"));
						record.blacksmith = Trim(xmlReader.GetAttribute("blacksmith"));
						record.weaponsmith = Trim(xmlReader.GetAttribute("weaponsmith"));

						record.townswoman = Trim(xmlReader.GetAttribute("townswoman"));
						record.townswoman_infant = Trim(xmlReader.GetAttribute("townswoman_infant"));
						record.townswoman_child = Trim(xmlReader.GetAttribute("townswoman_child"));
						record.townswoman_teenager = Trim(xmlReader.GetAttribute("townswoman_teenager"));
						record.townsman = Trim(xmlReader.GetAttribute("townsman"));
						record.townsman_infant = Trim(xmlReader.GetAttribute("townsman_infant"));
						record.townsman_child = Trim(xmlReader.GetAttribute("townsman_child"));
						record.townsman_teenager = Trim(xmlReader.GetAttribute("townsman_teenager"));
						record.villager = Trim(xmlReader.GetAttribute("villager"));
						record.village_woman = Trim(xmlReader.GetAttribute("village_woman"));//This may be in error in CSV to XML
						record.villager_male_child = Trim(xmlReader.GetAttribute("villager_male_child"));
						record.villager_male_teenager = Trim(xmlReader.GetAttribute("villager_male_teenager"));
						record.villager_female_child = Trim(xmlReader.GetAttribute("villager_female_child"));
						record.villager_female_teenager = Trim(xmlReader.GetAttribute("villager_female_teenager"));

						record.ransom_broker = Trim(xmlReader.GetAttribute("ransom_broker"));
						record.gangleader_bodyguard = Trim(xmlReader.GetAttribute("gangleader_bodyguard"));
						record.merchant_notary = Trim(xmlReader.GetAttribute("merchant_notary"));
						record.preacher_notary = Trim(xmlReader.GetAttribute("preacher_notary"));
						record.rural_notable_notary = Trim(xmlReader.GetAttribute("rural_notable_notary"));
						record.shop_worker = Trim(xmlReader.GetAttribute("shop_worker"));
						record.tavernkeeper = Trim(xmlReader.GetAttribute("tavernkeeper"));
						record.taverngamehost = Trim(xmlReader.GetAttribute("taverngamehost"));
						record.musician = Trim(xmlReader.GetAttribute("musician"));
						record.tavern_wench = Trim(xmlReader.GetAttribute("tavern_wench"));

						record.armorer = Trim(xmlReader.GetAttribute("armorer"));
						record.horseMerchant = Trim(xmlReader.GetAttribute("horseMerchant"));
						record.barber = Trim(xmlReader.GetAttribute("barber"));
						record.merchant = Trim(xmlReader.GetAttribute("merchant"));

						record.beggar = Trim(xmlReader.GetAttribute("beggar"));
						record.female_beggar = Trim(xmlReader.GetAttribute("female_beggar"));
						record.female_dancer = Trim(xmlReader.GetAttribute("female_dancer"));
						record.gear_practice_dummy = Trim(xmlReader.GetAttribute("gear_practice_dummy"));
						record.weapon_practice_stage_1 = Trim(xmlReader.GetAttribute("weapon_practice_stage_1"));
						record.weapon_practice_stage_2 = Trim(xmlReader.GetAttribute("weapon_practice_stage_2"));
						record.weapon_practice_stage_3 = Trim(xmlReader.GetAttribute("weapon_practice_stage_3"));
						record.gear_dummy = Trim(xmlReader.GetAttribute("gear_dummy"));

						record.bandit_bandit = Trim(xmlReader.GetAttribute("bandit_bandit"));
						record.bandit_chief = Trim(xmlReader.GetAttribute("bandit_chief"));
						record.bandit_raider = Trim(xmlReader.GetAttribute("bandit_raider"));
						record.bandit_boss = Trim(xmlReader.GetAttribute("bandit_boss"));
						record.board_game_type = xmlReader.GetAttribute("board_game_type");

						int c0 = 0;
						int c1 = 0;
						int c2 = 0;

					Lazyloop:
						while (xmlReader.Read() && !xmlReader.NodeType.Equals(XmlNodeType.Element)) ;
						if (!xmlReader.Name.Equals("name")) Console.WriteLine("\t {0}", xmlReader.Name);
						LazyloopSwitch:
						switch (xmlReader.Name) {
							case "Culture":
								records.Add(record);

								if (!xmlReader.NodeType.Equals(XmlNodeType.EndElement)) goto NewPartyRecord;
								continue;
							case "tournament_team_templates_one_participant":
								while (xmlReader.Read() && !xmlReader.NodeType.Equals(XmlNodeType.Element)) ;
								Console.WriteLine("\t >{0}", xmlReader.Name);
								if (!xmlReader.Name.Equals("template")) {
									if (xmlReader.Name.Equals("Culture")) goto case "Culture";
									goto LazyloopSwitch;
								}

								switch (c0) {
									case 0:
										record.tournament_template_one_participant_set_v1 = Trim(xmlReader.GetAttribute("name"));
										break;
									case 1:
										record.tournament_template_one_participant_set_v2 = Trim(xmlReader.GetAttribute("name"));
										break;
									case 2:
										record.tournament_template_one_participant_set_v3 = Trim(xmlReader.GetAttribute("name"));
										break;
									case 3:
										record.tournament_template_one_participant_set_v4 = Trim(xmlReader.GetAttribute("name"));
										break;
								}

								c0++;
								goto case "tournament_team_templates_one_participant";
							case "tournament_team_templates_two_participant":
								while (xmlReader.Read() && !xmlReader.NodeType.Equals(XmlNodeType.Element)) ;
								Console.WriteLine("\t >>{0}", xmlReader.Name);
								if (!xmlReader.Name.Equals("template")) {
									if (xmlReader.Name.Equals("Culture")) goto case "Culture";
									goto LazyloopSwitch;
								}

								switch (c1) {
									case 0:
										record.tournament_template_two_participant_set_v1 = Trim(xmlReader.GetAttribute("name"));
										break;
									case 1:
										record.tournament_template_two_participant_set_v2 = Trim(xmlReader.GetAttribute("name"));
										break;
									case 2:
										record.tournament_template_two_participant_set_v3 = Trim(xmlReader.GetAttribute("name"));
										break;
									case 3:
										record.tournament_template_two_participant_set_v4 = Trim(xmlReader.GetAttribute("name"));
										break;
								}

								c1++;
								goto case "tournament_team_templates_two_participant";
							case "tournament_team_templates_four_participant":
								while (xmlReader.Read() && !xmlReader.NodeType.Equals(XmlNodeType.Element)) ;
								Console.WriteLine("\t >>>{0}", xmlReader.Name);
								if (!xmlReader.Name.Equals("template")) {
									if (xmlReader.Name.Equals("Culture")) goto case "Culture";
									goto LazyloopSwitch;
								}

								switch (c2) {
									case 0:
										record.tournament_template_four_participant_set_v1 = Trim(xmlReader.GetAttribute("name"));
										break;
									case 1:
										record.tournament_template_four_participant_set_v2 = Trim(xmlReader.GetAttribute("name"));
										break;
									case 2:
										record.tournament_template_four_participant_set_v3 = Trim(xmlReader.GetAttribute("name"));
										break;
									case 3:
										record.tournament_template_four_participant_set_v4 = Trim(xmlReader.GetAttribute("name"));
										break;
								}

								c2++;
								goto case "tournament_team_templates_four_participant";
						}
						if (xmlReader.EOF) {
							if (!records.Contains(record)) records.Add(record);
							break;
						}
						goto Lazyloop;
						/*
						if (!xmlReader.Name.Equals("stacks")) Console.WriteLine("Error: Node name {0} of type {2} belonging to {1} was unexpected!", xmlReader.Name, xmlReader.GetAttribute("id"), xmlReader.NodeType.ToString());

						while (xmlReader.Read() && !(xmlReader.NodeType.Equals(XmlNodeType.EndElement) && xmlReader.Name.Equals("stacks"))) {
							if (!xmlReader.NodeType.Equals(XmlNodeType.Element)) continue;
							if (xmlReader.Name.Equals("stacks")) continue;
						}
						*/
					}
				}
				using (CsvWriter csvWriter = new CsvWriter(new StreamWriter(csvOutput), CultureInfo.InvariantCulture)) {
					csvWriter.WriteRecords(records);
					csvWriter.Flush();
				}
			}
		}


		public class CultureRecord {
			public string id { get; set; }
			public string name { get; set; }
			public string is_main_culture { get; set; }
			public string can_have_settlement { get; set; }
			public string town_edge_number { get; set; }
			public string militia_bonus { get; set; }
			public string prosperity_bonus { get; set; }
			public string encounter_background_mesh { get; set; }
			public string basic_troop { get; set; }
			public string elite_basic_troop { get; set; }

			public string is_bandit { get; set; }
			public string default_face_key { get; set; }

			public string default_party_template { get; set; }
			public string villager_party_template { get; set; }
			public string elite_caravan_party_template { get; set; }
			public string bandit_boss_party_template { get; set; }
			public string caravan_party_template { get; set; }
			public string militia_party_template { get; set; }
			public string rebels_party_template { get; set; }

			public string melee_militia_troop { get; set; }
			public string melee_elite_militia_troop { get; set; }
			public string ranged_militia_troop { get; set; }
			public string ranged_elite_militia_troop { get; set; }
			public string tournament_master { get; set; }
			public string caravan_master { get; set; }
			public string armed_trader { get; set; }
			public string caravan_guard { get; set; }
			public string veteran_caravan_guard { get; set; }

			public string duel_preset { get; set; }
			public string prison_guard { get; set; }
			public string guard { get; set; }
			public string steward { get; set; }
			public string blacksmith { get; set; }
			public string weaponsmith { get; set; }
			public string townswoman { get; set; }
			public string townswoman_infant { get; set; }
			public string townswoman_child { get; set; }
			public string townswoman_teenager { get; set; }
			public string townsman { get; set; }
			public string townsman_infant { get; set; }
			public string townsman_child { get; set; }
			public string townsman_teenager { get; set; }
			public string villager { get; set; }
			public string village_woman { get; set; }
			public string villager_male_child { get; set; }
			public string villager_male_teenager { get; set; }
			public string villager_female_child { get; set; }
			public string villager_female_teenager { get; set; }

			public string ransom_broker { get; set; }
			public string gangleader_bodyguard { get; set; }
			public string merchant_notary { get; set; }
			public string preacher_notary { get; set; }
			public string rural_notable_notary { get; set; }
			public string shop_worker { get; set; }
			public string tavernkeeper { get; set; }
			public string taverngamehost { get; set; }
			public string musician { get; set; }
			public string tavern_wench { get; set; }
			public string armorer { get; set; }
			public string horseMerchant { get; set; }
			public string barber { get; set; }
			public string merchant { get; set; }
			public string beggar { get; set; }
			public string female_beggar { get; set; }
			public string female_dancer { get; set; }
			public string gear_practice_dummy { get; set; }
			public string weapon_practice_stage_1 { get; set; }
			public string weapon_practice_stage_2 { get; set; }
			public string weapon_practice_stage_3 { get; set; }
			public string gear_dummy { get; set; }
			public string bandit_bandit { get; set; }
			public string bandit_chief { get; set; }
			public string bandit_raider { get; set; }
			public string bandit_boss { get; set; }
			public string board_game_type { get; set; }

			public string tournament_template_one_participant_set_v1 { get; set; }
			public string tournament_template_one_participant_set_v2 { get; set; }
			public string tournament_template_one_participant_set_v3 { get; set; }
			public string tournament_template_one_participant_set_v4 { get; set; }
			public string tournament_template_two_participant_set_v1 { get; set; }
			public string tournament_template_two_participant_set_v2 { get; set; }
			public string tournament_template_two_participant_set_v3 { get; set; }
			public string tournament_template_two_participant_set_v4 { get; set; }
			public string tournament_template_four_participant_set_v1 { get; set; }
			public string tournament_template_four_participant_set_v2 { get; set; }
			public string tournament_template_four_participant_set_v3 { get; set; }
			public string tournament_template_four_participant_set_v4 { get; set; }

			public string male_names { get; set; }
			public string female_names { get; set; }
			public string clan_names { get; set; }

			public string str_culture_description { get; set; }
			public string str_culture_rich_name { get; set; }
			public string str_faction_official { get; set; }
			public string str_faction_official_f { get; set; }
			public string str_faction_ruler { get; set; }
			public string str_faction_ruler_f { get; set; }
			public string str_faction_ruler_term_in_speech { get; set; }
			public string str_faction_ruler_name_with_title { get; set; }
			public string str_faction_noble_name_with_title { get; set; }
			public string str_adjective_for_faction { get; set; }
			public string str_short_term_for_faction { get; set; }
			public string str_faction_formal_name_for_culture { get; set; }
			public string str_neutral_term_for_culture { get; set; }


		}
	}
}
