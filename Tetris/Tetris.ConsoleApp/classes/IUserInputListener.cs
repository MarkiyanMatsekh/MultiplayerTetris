using System;

namespace Tetris.Contracts
{
    /// <summary>
    /// Describes the contract for the user input listener implementation.
    /// </summary>
    public interface IUserInputListener : IDisposable
    {
        /// <summary>
        /// Gets a value that indicates whether <see cref="T:Tetris.Contracts.IUserInputListener"/> has been started.
        /// </summary>
        Boolean IsListening { get; }

        /// <summary>
        /// Causes this instance to start capturing user input.
        /// </summary>
        void Start();

        /// <summary>
        /// Causes this instance to stop receiving user input.
        /// </summary>
        void End();

        /// <summary>
        /// Occurs when the user is performing a game command.
        /// </summary>
        event Action<string> OnUserInput;
    }
}