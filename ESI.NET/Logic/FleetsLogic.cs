﻿using ESI.NET.Enumerations;
using ESI.NET.Models.Fleets;
using ESI.NET.Models.SSO;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static ESI.NET.EsiRequest;

namespace ESI.NET.Logic
{
    public class FleetsLogic
    {
        private HttpClient _client;
        private ESIConfig _config;
        private AuthorizedCharacterData _data;
        private int character_id;

        public FleetsLogic(HttpClient client, ESIConfig config, AuthorizedCharacterData data = null)
        {
            _client = client;
            _config = config;
            _data = data;

            if (data != null)
                character_id = data.CharacterID;
        }

        /// <summary>
        /// /fleets/{fleet_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <returns></returns>
        public async Task<EsiResponse<Settings>> Settings(long fleet_id)
            => await Execute<Settings>(_client, _config, RequestSecurity.Authenticated, RequestMethod.GET, $"/fleets/{fleet_id}/", token: _data.Token);

        /// <summary>
        /// /fleets/{fleet_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="motd"></param>
        /// <param name="is_free_move"></param>
        /// <returns></returns>
        public async Task<EsiResponse<string>> UpdateSettings(long fleet_id, string motd = null, bool? is_free_move = null)
        {
            dynamic body = null;

            if (motd != null)
                body = new { motd = motd };
            if (is_free_move != null)
                body = new { is_free_move = is_free_move };
            if (motd != null && is_free_move != null)
                body = new { motd = motd, is_free_move = is_free_move };

            var response = await Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.PUT, $"/fleets/{fleet_id}/", body: body, token: _data.Token);

            if (response.StatusCode == HttpStatusCode.NoContent)
                response.Message = Dictionaries.NoContentMessages["PUT|/fleets/{fleet_id}/"];

            return response;
        }

        /// <summary>
        /// /characters/{character_id}/fleet/
        /// </summary>
        /// <returns></returns>
        public async Task<EsiResponse<FleetInfo>> FleetInfo()
            => await Execute<FleetInfo>(_client, _config, RequestSecurity.Authenticated, RequestMethod.GET, $"/characters/{character_id}/fleet/", token: _data.Token);

        /// <summary>
        /// /fleets/{fleet_id}/members/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <returns></returns>
        public async Task<EsiResponse<List<Member>>> Members(long fleet_id)
            => await Execute<List<Member>>(_client, _config, RequestSecurity.Authenticated, RequestMethod.GET, $"/fleets/{fleet_id}/members/", token: _data.Token);

        /// <summary>
        /// /fleets/{fleet_id}/members/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="character_id"></param>
        /// <param name="role"></param>
        /// <param name="wing_id"></param>
        /// <param name="squad_id"></param>
        /// <returns></returns>
        public async Task<EsiResponse<string>> InviteCharacter(long fleet_id, int character_id, FleetRole role, long wing_id = 0, long squad_id = 0)
        {
            dynamic body = null;
            body = BuildFleetInvite(character_id, role, wing_id, squad_id, body);

            var response = await Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.POST, $"/fleets/{fleet_id}/members/", body: body, token: _data.Token);

            if (response.StatusCode == HttpStatusCode.NoContent)
                response.Message = Dictionaries.NoContentMessages["POST|/fleets/{fleet_id}/members/"];

            return response;
        }

        /// <summary>
        /// /fleets/{fleet_id}/members/{member_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="member_id"></param>
        /// <param name="role"></param>
        /// <param name="wing_id"></param>
        /// <param name="squad_id"></param>
        /// <returns></returns>
        public async Task<EsiResponse<string>> MoveCharacter(long fleet_id, int member_id, FleetRole role, long wing_id = 0, long squad_id = 0)
        {
            dynamic body = null;
            body = BuildFleetInvite(character_id, role, wing_id, squad_id, body);

            var response = await Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.PUT, $"/fleets/{fleet_id}/members/{member_id}/", body: body, token: _data.Token);

            if (response.StatusCode == HttpStatusCode.NoContent)
                response.Message = Dictionaries.NoContentMessages["PUT|/fleets/{fleet_id}/members/{member_id}/"];

            return response;
        }

        /// <summary>
        /// /fleets/{fleet_id}/members/{member_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public async Task<EsiResponse<string>> KickCharacter(long fleet_id, int member_id)
        {
            var response = await Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.DELETE, $"/fleets/{fleet_id}/members/{member_id}/", token: _data.Token);

            if (response.StatusCode == HttpStatusCode.NoContent)
                response.Message = Dictionaries.NoContentMessages["DELETE|/fleets/{fleet_id}/members/{member_id}/"];

            return response;
        }

        /// <summary>
        /// /fleets/{fleet_id}/wings/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <returns></returns>
        public async Task<EsiResponse<List<Wing>>> Wings(long fleet_id)
        {
            var response = await Execute<List<Wing>>(_client, _config, RequestSecurity.Authenticated, RequestMethod.GET, $"/fleets/{fleet_id}/wings/", token: _data.Token);

            if (response.StatusCode == HttpStatusCode.NoContent)
                response.Message = Dictionaries.NoContentMessages["DELETE|/fleets/{fleet_id}/members/{member_id}/"];

            return response;
        }

        /// <summary>
        /// /fleets/{fleet_id}/wings/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <returns></returns>
        public async Task<EsiResponse<NewWing>> CreateWing(long fleet_id)
            => await Execute<NewWing>(_client, _config, RequestSecurity.Authenticated, RequestMethod.POST, $"/fleets/{fleet_id}/wings/", token: _data.Token);

        /// <summary>
        /// /fleets/{fleet_id}/wings/{wing_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="wing_id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<EsiResponse<string>> RenameWing(long fleet_id, long wing_id, string name)
        {
            var response = await Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.PUT, $"/fleets/{fleet_id}/wings/{wing_id}/", body: new
            {
                name
            }, token: _data.Token);

            if (response.StatusCode == HttpStatusCode.NoContent)
                response.Message = Dictionaries.NoContentMessages["PUT|/fleets/{fleet_id}/wings/{wing_id}/"];

            return response;
        }

        /// <summary>
        /// /fleets/{fleet_id}/wings/{wing_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="wing_id"></param>
        /// <returns></returns>
        public async Task<EsiResponse<string>> DeleteWing(long fleet_id, long wing_id)
        {
            var response = await Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.DELETE, $"/fleets/{fleet_id}/wings/{wing_id}/", token: _data.Token);

            if (response.StatusCode == HttpStatusCode.NoContent)
                response.Message = Dictionaries.NoContentMessages["DELETE|/fleets/{fleet_id}/wings/{wing_id}/"];

            return response;
        }

        /// <summary>
        /// /fleets/{fleet_id}/wings/{wing_id}/squads/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="wing_id"></param>
        /// <returns></returns>
        public async Task<EsiResponse<NewSquad>> CreateSquad(long fleet_id, long wing_id)
            => await Execute<NewSquad>(_client, _config, RequestSecurity.Authenticated, RequestMethod.POST, $"/fleets/{fleet_id}/wings/{wing_id}/squads/", token: _data.Token);

        /// <summary>
        /// /fleets/{fleet_id}/squads/{squad_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="squad_id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<EsiResponse<string>> RenameSquad(long fleet_id, long squad_id, string name)
        {
            var response = await Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.PUT, $"/fleets/{fleet_id}/squads/{squad_id}/", body: new
            {
                name
            }, token: _data.Token);

            if (response.StatusCode == HttpStatusCode.NoContent)
                response.Message = Dictionaries.NoContentMessages["PUT|/fleets/{fleet_id}/squads/{squad_id}/"];

            return response;
        }

        /// <summary>
        /// /fleets/{fleet_id}/squads/{squad_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="squad_id"></param>
        /// <returns></returns>
        public async Task<EsiResponse<string>> DeleteSquad(long fleet_id, long squad_id)
        {
            var response = await Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.DELETE, $"/fleets/{fleet_id}/squads/{squad_id}/", token: _data.Token);

            if (response.StatusCode == HttpStatusCode.NoContent)
                response.Message = Dictionaries.NoContentMessages["DELETE|/fleets/{fleet_id}/squads/{squad_id}/"];

            return response;
        }



        /// <summary>
        /// Dynamically builds the required structure for a fleet invite or move
        /// </summary>
        /// <param name="character_id"></param>
        /// <param name="role"></param>
        /// <param name="wing_id"></param>
        /// <param name="squad_id"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private static dynamic BuildFleetInvite(int character_id, FleetRole role, long wing_id, long squad_id, dynamic body)
        {
            if (role == FleetRole.FleetCommander)
                body = new { character_id, role = role.ToString() };

            else if (role == FleetRole.WingCommander)
                body = new { character_id, role = role.ToString(), wing_id };

            else if (role == FleetRole.SquadCommander || role == FleetRole.SquadMember)
                body = new { character_id, role = role.ToString(), wing_id, squad_id };

            return body;
        }
    }
}