require 'yaml'

puts Dir.pwd

new_hash = ARGV[0].to_s

config = YAML.load_file('_config.yml')

puts config['git_hash']
config['git_hash'] = new_hash
File.open('_config.yml', 'w') do |h|
	h.write config.to_yaml
end

