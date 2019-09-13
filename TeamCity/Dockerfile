FROM jetbrains/teamcity-agent:latest

ADD domainca.crt /usr/local/share/ca-certificates/domainca.crt
RUN chmod 644 /usr/local/share/ca-certificates/domainca.crt && update-ca-certificates