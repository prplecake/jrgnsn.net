Jekyll::Hooks.register :posts, :post_write do |post|
  all_existing_tags = Dir.entries("_tags")
                         .map { |t| t.match(/(.*).md/) }
                         .compact.map { |m| m[1] }

  tags = post['tags'].reject { |t| t.empty? }
  puts "Generating tags..."
  generate_tags_files(tags)
end

def generate_tags_files(tags)
  tags.each do |tag|
    slug = tag.downcase.strip.gsub(' ', '-').gsub(/[^\w-]/, '')
    # generate tag file
    puts "Generating tag file for #{slug}..."
    if !File.exist?("_tags/#{slug}.md")
      puts "Creating tag file for #{slug}..."
      File.open("_tags/#{slug}.md", "wb") do |file|
        file << "---\nlayout: tags\ntag-name: #{slug}\n---\n"
      end
    end
    # generate feed file
    puts "Generating feed file for #{slug}..."
    if !File.exist?("_feeds/#{slug}.xml")
      puts "Creating feed file for #{slug}..."
      File.open("_feeds/#{slug}.xml", "wb") do |file|
        file << "---\nlayout: feed\ntag-name: #{slug}\n---\n"
      end
    end
  end
end