﻿using ESI.NET.Models.PlanetaryInteraction;
using ESI.NET.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static ESI.NET.EsiRequest;

namespace ESI.NET.Logic
{
    public class PlanetaryInteractionLogic
    {
        private HttpClient _client;
        private ESIConfig _config;
        private AuthorizedCharacterData _data;
        private int character_id, corporation_id;

        public PlanetaryInteractionLogic(HttpClient client, ESIConfig config, AuthorizedCharacterData data = null)
        {
            _client = client;
            _config = config;
            _data = data;

            if (data != null)
            {
                character_id = data.CharacterID;
                corporation_id = data.CorporationID;
            }
        }

        /// <summary>
        /// /characters/{character_id}/planets/
        /// </summary>
        /// <returns></returns>
        public async Task<EsiResponse<List<Planet>>> Colonies()
            => await Execute<List<Planet>>(_client, _config, RequestSecurity.Authenticated, RequestMethod.GET, $"/characters/{character_id}/planets/", token: _data.Token);

        /// <summary>
        /// /characters/{character_id}/planets/{planet_id}/
        /// </summary>
        /// <param name="planet_id"></param>
        /// <returns></returns>
        public async Task<EsiResponse<ColonyLayout>> ColonyLayout(int planet_id)
            => await Execute<ColonyLayout>(_client, _config, RequestSecurity.Authenticated, RequestMethod.GET, $"/characters/{character_id}/planets/{planet_id}/", token: _data.Token);

        /// <summary>
        /// /corporations/{corporation_id}/customs_offices/
        /// </summary>
        /// <returns></returns>
        public async Task<EsiResponse<List<CustomsOffice>>> CorporationCustomsOffices()
            => await Execute<List<CustomsOffice>>(_client, _config, RequestSecurity.Authenticated, RequestMethod.GET, $"/corporations/{corporation_id}/customs_offices/", token: _data.Token);

        /// <summary>
        /// /universe/schematics/{schematic_id}/
        /// </summary>
        /// <param name="schematic_id"></param>
        /// <returns></returns>
        public async Task<EsiResponse<Schematic>> SchematicInformation(int schematic_id)
            => await Execute<Schematic>(_client, _config, RequestSecurity.Public, RequestMethod.GET, $"/universe/schematics/{schematic_id}/");
    }
}