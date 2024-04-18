Jekyll::Hooks.register :posts, :post_write do |post|
  all_existing_tags = Dir.entries("_tags")
                         .map { |t| t.match(/(.*).md/) }
                         .compact.map { |m| m[1] }

  tags = post['tags'].reject { |t| t.empty? }
  generate_tags_files(tags)
end

def generate_tags_files(tags)
  tags.each do |tag|
    slug = tag.downcase.strip.gsub(' ', '-').gsub(/[^\w-]/, '')
    # generate tag file
    File.open("_tags/#{slug}.md", "wb") do |file|
      file << "---\nlayout: tags\ntag-name: #{slug}\n---\n"
    end
    # generate feed file
    File.open("_feeds/#{slug}.xml", "wb") do |file|
      file << "---\nlayout: feed\ntag-name: #{slug}\n---\n"
    end
  end
end