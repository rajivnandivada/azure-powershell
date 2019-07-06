// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.Azure.PowerShell.Cmdlets.ManagedServices.Models
{
    using Microsoft.Azure.Commands.Common.Authentication;
    using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
    using Microsoft.Azure.Management.ManagedServices;
    using Microsoft.Rest.Azure;
    using System;
    using Microsoft.Azure.Management.ManagedServices.Models;
    using Microsoft.Azure.Commands.ResourceManager.Common.Properties;
    /// <summary>
    /// Low-level API implementation for the ManagedServicesClient service.
    /// </summary>
    public class PSManagedServicesClient
    {
        protected const string API_VERSION = "2018-06-01-preview";
        public ManagedServicesClient ManagedServicesClient { get; private set; }

        public PSManagedServicesClient()
        {

        }

        public PSManagedServicesClient(IAzureContext context)
        {
            if (context == null)
            {
                throw new ApplicationException(Resources.InvalidDefaultSubscription);
            }

            this.ManagedServicesClient = AzureSession.Instance.ClientFactory.CreateArmClient<ManagedServicesClient>(
                context,
                AzureEnvironment.Endpoint.ResourceManager);
        }

        #region Registration Assignment

        public RegistrationAssignment GetRegistrationAssignment(
            string scope,
            string registrationAssignmentId,
            bool? expandRegistrationDefinition = null)
        {
            return this.ManagedServicesClient.RegistrationAssignments.Get(
                scope: scope,
                registrationAssignmentId: registrationAssignmentId,
                expandRegistrationDefinition: expandRegistrationDefinition);
        }

        public IPage<RegistrationAssignment> ListRegistrationAssignments(
            string scope,
            bool? expandRegistrationDefinition = null)
        {
            return this.ManagedServicesClient.RegistrationAssignments.List(
                scope: scope,
                expandRegistrationDefinition: expandRegistrationDefinition);
        }

        public RegistrationAssignment CreateOrUpdateRegistrationAssignment(
            string scope,
            string registrationDefinitionId,
            Guid registrationAssignmentId = default(Guid))
        {
            var registrationAssignment = new RegistrationAssignment
            {
                Properties = new RegistrationAssignmentProperties
                {
                    RegistrationDefinitionId = registrationDefinitionId,
                }
            };

            return this.ManagedServicesClient.RegistrationAssignments.CreateOrUpdate(
                scope: scope,
                registrationAssignmentId: registrationAssignmentId.ToString(),
                requestBody: registrationAssignment);
        }

        public void RemoveRegistrationAssignment(
            string scope, 
            string registrationAssignmentId)
        {
            this.ManagedServicesClient.RegistrationAssignments.Delete(
                scope: scope,
                registrationAssignmentId: registrationAssignmentId);
        }

        #endregion

        #region RegistrationDefinitions

        public RegistrationDefinition CreateOrUpdateRegistrationDefinition(
            string scope,
            RegistrationDefinition registrationDefinition,
            Guid registratonDefinitionId = default(Guid))
        {
            return this.ManagedServicesClient.RegistrationDefinitions.CreateOrUpdate(
                scope: scope,
                registrationDefinitionId: registratonDefinitionId.ToString(),
                requestBody: registrationDefinition);
        }

        public IPage<RegistrationDefinition> ListRegistrationDefinitions(string scope)
        {
            return this.ManagedServicesClient.RegistrationDefinitions.List(
                scope: scope);
        }

        public RegistrationDefinition GetRegistrationDefinition(
            string scope, 
            string registrationDefinitionId)
        {
            return this.ManagedServicesClient.RegistrationDefinitions.Get(
                scope: scope,
                registrationDefinitionId: registrationDefinitionId);
        }

        public void RemoveRegistrationDefinition(
            string scope    , 
            string registrationDefinitionId)
        {
            this.ManagedServicesClient.RegistrationDefinitions.Delete(
                    scope: scope,
                    registrationDefinitionId: registrationDefinitionId);
        }

        #endregion
    }
}