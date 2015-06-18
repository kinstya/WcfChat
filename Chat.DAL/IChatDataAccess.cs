// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChatDataAccess.cs" company="">
//   
// </copyright>
// <summary>
//   The ChatDataAccess interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.DAL
{
    using System;

    using Chat.DAL.Info;
    using Chat.DAL.Models;

    /// <summary>
    /// The ChatDataAccess interface.
    /// </summary>
    public interface IChatDataAccess
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add message.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="participantId">
        /// The participant id.
        /// </param>
        void AddMessage(MessageInfo msg, int participantId);

        /// <summary>
        /// The add participant.
        /// </summary>
        /// <param name="clientInfo">
        /// The client Info.
        /// </param>
        /// <param name="conversationId">
        /// The conversation id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int AddParticipant(ClientInfo clientInfo, int conversationId);

        /// <summary>
        /// The end conversation.
        /// </summary>
        /// <param name="conversationId">
        /// The conversation id.
        /// </param>
        void EndConversation(int conversationId);

        /// <summary>
        /// The participant left.
        /// </summary>
        /// <param name="participantId">
        /// The participant id.
        /// </param>
        void ParticipantLeft(int participantId);

        /// <summary>
        /// The start conversation.
        /// </summary>
        /// <param name="time"></param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int StartConversation(DateTime time);

        #endregion

        /// <summary>
        /// The get participant by name.
        /// </summary>
        /// <param name="conversationId">
        /// The conversation id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="Participant"/>.
        /// </returns>
        ClientInfo GetParticipantByName(int conversationId, string name);
    }
}