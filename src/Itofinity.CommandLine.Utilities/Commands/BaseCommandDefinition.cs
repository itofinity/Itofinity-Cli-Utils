using System;
using System.Linq;
using System.Collections.Generic;
using Itofinity.CommandLine.Utilities.Options;

namespace Itofinity.CommandLine.Utilities.Commands
{
    public class BaseCommandDefinition : ICommandDefinition
    {
        private List<ICommandDefinition> _subCommands = new List<ICommandDefinition>();
        private List<IOption> _options = new List<IOption>();
        private Action _action = () => { /* do nothing */ };
        public BaseCommandDefinition(string name, string description, Action action, IEnumerable<ICommandDefinition> subCommands, IEnumerable<IOption> options)
        {
            Name = name;
            Description = description;
            _action = action;
            _subCommands = subCommands.ToList();
            _options = options.ToList();
        }

        public BaseCommandDefinition(string name, string description, Action action) : this(name, description, action, new List<ICommandDefinition>(), new List<IOption>())
        { 
            
        }

        public string Name { get; }

        public string Description { get; }

        public Action Action { get { return _action; } }

        public IEnumerable<ICommandDefinition> SubCommands { get { return _subCommands.AsReadOnly(); } }

        public IEnumerable<IOption> Options { get { return _options.AsReadOnly(); } }
    }
}